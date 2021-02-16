using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Angular;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Services;
using CodeShellCore.Text;
using CodeShellCore.Text.Localization;
using CodeShellCore.Text.ResourceReader;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace CodeShellCore.Moldster.Localization.Internal
{
    public class LocalizationService : ConsoleService, ILocalizationService
    {
        public bool SuspendOut { get; set; }
        protected int resultCol = 8;
        protected readonly WriterService _writer;
        protected readonly IPathsService _paths;
        protected readonly IMoldProvider _molds;
        protected readonly IConfigUnit _unit;

        public LocalizationService(
            IMoldProvider molds,
            IConfigUnit unit,
            IPathsService paths,
            IOutputWriter output) : base(output)
        {
            _writer = new WriterService();
            _paths = paths;
            _molds = molds;
            _unit = unit;
        }

        DataItem[] GetItems(string type, string locale)
        {
            ResxXmlReader reader = new ResxXmlReader();
            string resPath = Path.Combine(_paths.LocalizationRoot, "Localization", type + "." + locale + ".resx");
            if (reader.TryRead(resPath, out ResourceContainer cont1))
            {
                return cont1.DataItems ?? new DataItem[0];
            }
            return new DataItem[0];
        }

        string _resxToJson(string type, string loc, IEnumerable<CustomText> ten)
        {
            var items = GetItems(type, loc);
            var typeEnum = (TextTypes)Enum.Parse(typeof(TextTypes), type);
            var data = ten.Where(e => e.Type == (int)typeEnum && e.Locale == loc).ToDictionary(d => d.Code, d => d.Value);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in items)
            {
                if (data.TryGetValue(item.Name, out string val))
                {
                    dic[item.Name] = val;
                }
                else if (!string.IsNullOrEmpty(item.Value))
                {
                    dic[item.Name] = item.Value;
                }
                else
                {
                    dic[item.Name] = LangUtils.IdToPhrase(item.Name);
                }

            }
            return dic.ToJson(Formatting.Indented);
        }

        public virtual void GenerateJsonFiles(string moduleCode)
        {
            if (string.IsNullOrEmpty(_paths.LocalizationRoot))
                return;

            Out.Write("Generating localization dictionaries : ");

            string template = _molds.LocaleLoaderMold;
            string[] locales = Shell.SupportedLanguages.ToArray();
            string[] types = new string[] { "Columns", "Words", "Pages", "Messages" };
            List<CustomText> ten = _unit.CustomTextRepository.GetForTenant(moduleCode);

            foreach (string loc in locales)
            {
                string loader = Path.Combine(_paths.UIRoot, moduleCode, "app\\Localization", loc, "Loader.ts");
                string contents = _writer.FillStringParameters(template, new { Locale = loc });

                Utils.CreateFolderForFile(loader);
                File.WriteAllText(loader, contents);

                foreach (string type in types)
                {
                    string data = _resxToJson(type, loc, ten);

                    string path = Path.Combine(_paths.UIRoot, moduleCode, "app\\Localization", loc, type + ".json");
                    Utils.CreateFolderForFile(path);
                    File.WriteAllText(path, data);
                }

            }

            GotoColumn(resultCol);
            WriteSuccess();

            Out.WriteLine();
        }

        void UnZip(byte[] bytes, string folder, string name, bool overwrite = false)
        {
            using (var t = SW.Measure())
            {
                string file = Path.Combine(folder, $"{name}.zip");
                string folderPath = Path.Combine(folder, name);
                bool write = true;

                if (Directory.Exists(folderPath))
                {
                    if (overwrite && Utils.DeleteDirectory(folderPath))
                    {
                        write = true;
                    }
                    else
                    {
                        Out.WriteLine(folderPath + " already exists");
                        return;
                    }
                }

                if (write)
                {
                    WriteFileOperation($"Extracting {name} to", folder, false);
                    Utils.CreateFolderForFile(file);
                    File.WriteAllBytes(file, bytes);
                    ZipFile.ExtractToDirectory(file, folderPath);
                    File.Delete(file);
                    WriteSuccess(t.Elapsed, SuccessCol);
                }
                Out.WriteLine();
            }
        }

        public virtual void AddLocalizationFiles()
        {
            UnZip(Properties.Resources.Localization, _paths.LocalizationRoot, "Localization");
        }

        public virtual void InitializeResxFiles()
        {

            using (var x = SW.Measure())
            {
                string[] types = new string[] { "Columns", "Words", "Pages", "Messages" };

                foreach (string type in types)
                {
                    foreach (var lang in Shell.SupportedLanguages)
                    {
                        var fileName = type + "." + lang + ".resx";
                        string filePath = Path.Combine(_paths.LocalizationRoot, "Localization", fileName);
                        if (!File.Exists(filePath))
                        {
                            Utils.CreateFolderForFile(filePath);
                            ResxXmlReader reader = new ResxXmlReader();
                            var headers = ResHeaderItem.Default;
                            reader.Save(filePath, new ResourceContainer { DataItems = new DataItem[0], Headers = headers });
                            Out.WriteLine($"Created file [{fileName}]");
                        }
                    }
                }
                WriteSuccess(x.Elapsed);
            }
        }

        public virtual void SyncAllLanguages()
        {
            var doneLangs = new List<string>();
            foreach (var s in Shell.SupportedLanguages)
            {
                foreach (var s2 in Shell.SupportedLanguages)
                {
                    if (s != s2 && !doneLangs.Contains(s2))
                        SyncLanguages(s, s2);
                }
                doneLangs.Add(s);
            }
        }

        public virtual LoadResult<CustomText> LoadForTenant(CustomTextRequest req, LoadOptions opts)
        {
            string resLang1 = Path.Combine(_paths.LocalizationRoot, "Localization", ((TextTypes)req.Type).ToString() + "." + req.Locale + ".resx");

            ResxXmlReader reader = new ResxXmlReader();
            var res = new LoadResult<CustomText>();

            if (reader.TryRead(resLang1, out ResourceContainer cont1))
            {
                var items = cont1.DataItems.ToList();

                if (!string.IsNullOrEmpty(opts.SearchTerm))
                {
                    items = items.Where(e => e.Name.ToLower().Contains(opts.SearchTerm.ToLower()) || e.Value.ToLower().Contains(opts.SearchTerm.ToLower())).ToList();
                }
                res.TotalCount = items.Count();

                IEnumerable<DataItem> q;
                if (opts.Showing > 0)
                {
                    q = items.Skip(opts.Skip).Take(opts.Showing);
                }
                else
                {
                    q = items;
                }

                res.List = q.Select(e => new CustomText
                {
                    Code = e.Name,
                    Value = e.Value,
                    TenantId = req.TenantId,
                    State = "Detached",
                    Locale = req.Locale,
                    Type = req.Type
                }).ToList();

            }
            return res;


        }

        public virtual void Import(string type, string lang, List<DataItem> strs, bool suspendOut = false)
        {
            string resLang1 = Path.Combine(_paths.LocalizationRoot, "Localization", type + "." + lang + ".resx");

            ResxXmlReader reader = new ResxXmlReader();


            var data1 = new List<DataItem>();
            var headers1 = new ResHeaderItem[0];

            if (reader.TryRead(resLang1, out ResourceContainer cont1))
            {
                cont1.DataItems = cont1.DataItems ?? new DataItem[0];
                if (!suspendOut)
                    Out.WriteLine("Found " + type + "." + lang + ".resx with " + cont1.DataItems.Length + " items");
                headers1 = cont1.Headers;
                data1 = new List<DataItem>();
                data1.AddRange(cont1.DataItems);
            }

            foreach (var item in strs)
            {
                if (!string.IsNullOrEmpty(item.Name))
                {
                    var ex = data1.FirstOrDefault(d => d.Name == item.Name);
                    if (ex == null)
                    {
                        data1.Add(new DataItem
                        {
                            Name = item.Name.Trim(),
                            Value = string.IsNullOrEmpty(item.Value) ? "" : item.Value.Trim(),
                            Space = "preserve"
                        });
                    }
                    else if (!string.IsNullOrEmpty(item.Value))
                    {
                        ex.Value = item.Value;
                    }
                }
            }
            reader.Save(resLang1, new ResourceContainer { DataItems = data1.ToArray(), Headers = headers1 });
        }

        protected void SaveFile(string type, string lang, DataItem[] lst)
        {
            string resLang1 = Path.Combine(_paths.LocalizationRoot, "Localization", type + "." + lang + ".resx");
            ResxXmlReader reader = new ResxXmlReader();
            var headers1 = new ResHeaderItem[0];
            if (reader.TryRead(resLang1, out ResourceContainer cont1))
            {
                headers1 = cont1.Headers;
            }
            reader.Save(resLang1, new ResourceContainer { DataItems = lst, Headers = headers1 });
        }

        public virtual void SyncLanguages(string lang1, string lang2)
        {
            using (var x = SW.Measure())
            {
                string[] types = new string[] { "Columns", "Words", "Pages", "Messages" };

                foreach (string type in types)
                {
                    string resLang1 = Path.Combine(_paths.LocalizationRoot, "Localization", type + "." + lang1 + ".resx");
                    string resLang2 = Path.Combine(_paths.LocalizationRoot, "Localization", type + "." + lang2 + ".resx");

                    ResxXmlReader reader = new ResxXmlReader();

                    var data1 = new List<DataItem>();
                    var data2 = new List<DataItem>();

                    var headers1 = new ResHeaderItem[0];
                    var headers2 = new ResHeaderItem[0];

                    if (reader.TryRead(resLang1, out ResourceContainer cont1))
                    {
                        cont1.DataItems = cont1.DataItems ?? new DataItem[0];
                        Out.WriteLine("Found " + type + "." + lang1 + ".resx with " + cont1.DataItems.Length + " items");
                        headers1 = cont1.Headers;
                        data1 = new List<DataItem>();
                        data1.AddRange(cont1.DataItems);
                    }

                    if (reader.TryRead(resLang2, out ResourceContainer cont2))
                    {
                        cont2.DataItems = cont2.DataItems ?? new DataItem[0];
                        Out.WriteLine("Found " + type + "." + lang2 + ".resx with " + cont2.DataItems.Length + " items");
                        headers2 = cont2.Headers;
                        data2 = new List<DataItem>();
                        data2.AddRange(cont2.DataItems);
                    }

                    int i = 0;
                    foreach (var item in data1)
                    {
                        if (!data2.Any(d => d.Name == item.Name))
                        {
                            data2.Add(new DataItem
                            {
                                Name = item.Name,
                                Value = "",
                                Space = item.Space
                            });
                            i++;
                        }

                    }
                    Out.WriteLine($"{lang1} --> {lang2} : Added {i} Entries..");

                    i = 0;
                    foreach (var item in data2)
                    {
                        if (!data1.Any(d => d.Name == item.Name))
                        {
                            data1.Add(new DataItem
                            {
                                Name = item.Name,
                                Value = type == "Messages" ? LangUtils.IdToPhrase(item.Name) : "",
                                Space = item.Space
                            });
                            i++;
                        }

                    }
                    Out.WriteLine($"{lang2} --> {lang1} : Added {i} Entries..");
                    reader.Save(resLang1, new ResourceContainer { DataItems = data1.ToArray(), Headers = headers1 });
                    reader.Save(resLang2, new ResourceContainer { DataItems = data2.ToArray(), Headers = headers2 });
                }



                WriteSuccess(x.Elapsed);
            }
        }

        public virtual void UpdateFiles(LocalizationDataCollector localization)
        {
            var items = new List<DataItem>();
            string loc = Shell.DefaultCulture.TwoLetterISOLanguageName;
            foreach (var i in localization.Words)
            {
                items.Add(new DataItem { Name = i, Value = "" });
            }
            Import("Words", loc, items, true);

            items = new List<DataItem>();
            foreach (var i in localization.Messages)
            {
                items.Add(new DataItem { Name = i, Value = "" });
            }
            Import("Messages", loc, items, true);

            items = new List<DataItem>();
            foreach (var i in localization.Columns)
            {
                items.Add(new DataItem { Name = i, Value = "" });
            }
            Import("Columns", loc, items, true);

            items = new List<DataItem>();
            foreach (var i in localization.Pages)
            {
                items.Add(new DataItem { Name = i, Value = "" });
            }
            Import("Pages", loc, items, true);
        }

        public virtual void FixPages(string tenantCode)
        {
            foreach (var loc in Shell.SupportedLanguages)
            {
                int newItems = 0;
                Out.Write("Fixing for [" + loc + "]");
                Out.GotoColumn(SuccessCol);
                var items = GetItems("Pages", loc);
                List<DataItem> newList = new List<DataItem>();
                List<PageIdentifierDTO> data = _unit.PageRepository.GetDistinctIdentifiers();
                foreach (var item in items)
                {
                    var pageName = item.Name.GetAfterFirst("__");
                    var domainName = item.Name.GetBeforeFirst("__");
                    if (!string.IsNullOrEmpty(item.Value))
                    {
                        var similar = data.Where(e => e.Page == pageName).ToList();
                        foreach (var sim in similar)
                        {
                            var existing = items.Where(e => e.Name == sim.Domain + "__" + sim.Page).FirstOrDefault();
                            if (existing == null)
                            {
                                newItems++;
                                newList.Add(new DataItem
                                {
                                    Name = sim.Domain + "__" + sim.Page,
                                    Value = item.Value,
                                    Space = "preserve"
                                });
                            }
                            else
                            {
                                existing.Value = item.Value;
                            }
                        }
                    }
                    var inDb = data.Any(e => e.Domain == domainName && e.Page == pageName);
                    if (inDb)
                    {

                        newList.Add(item);
                    }
                }

                foreach (var page in data)
                {
                    var key = page.Domain + "__" + page.Page;
                    if (!items.Any(e => e.Name == key) && !newList.Any(e => e.Name == key))
                    {
                        newItems++;
                        newList.Add(new DataItem { Name = key, Value = "", Space = "preserve" });
                    }
                }
                SaveFile("Pages", loc, newList.ToArray());
                WriteColored("Success [Added : " + newItems + "]", ConsoleColor.Green);
                Out.WriteLine();
            }
        }
    }
}
