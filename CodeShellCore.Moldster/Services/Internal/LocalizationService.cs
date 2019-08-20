using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Angular;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Services;
using CodeShellCore.Text.Localization;
using CodeShellCore.Text.ResourceReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeShellCore.Moldster.Services.Internal
{
    public class LocalizationService : ConsoleService, ILocalizationService
    {
        protected int resultCol = 8;
        readonly WriterService _writer;
        readonly PathProvider _paths;
        readonly IMoldProvider _molds;
        public LocalizationService(
            WriterService wt,
            IMoldProvider molds,
            PathProvider paths)
        {
            _writer = wt;
            _paths = paths;
            _molds = molds;
        }
        public void GenerateJsonFiles(string moduleCode)
        {

            Console.Write("Generating localization dictionaries : ");

            string template = _molds.LocaleLoaderMold;
            string[] locales = new string[] { "ar", "en" };
            string[] types = new string[] { "Columns", "Words", "Pages", "Messages" };

            foreach (string loc in locales)
            {
                string loader = Path.Combine(_paths.UIRoot, moduleCode, "app\\Localization", loc, "Loader.ts");
                string contents = _writer.FillStringParameters(template, new { Locale = loc });

                Utils.CreateFolderForFile(loader);
                File.WriteAllText(loader, contents);

                foreach (string type in types)
                {
                    string path = Path.Combine(_paths.UIRoot, moduleCode, "app\\Localization", loc, type + ".json");
                    string resPath = Path.Combine(_paths.LocalizationRoot, "Localization", type + "." + loc + ".resx");
                    Utils.CreateFolderForFile(path);
                    string data = LangUtils.ResourceToJson(resPath);
                    File.WriteAllText(path, data);
                }

            }

            GotoColumn(resultCol);
            WriteSuccess();

            Console.WriteLine();
        }

        

        public void InitializeResxFiles()
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
                            Console.WriteLine($"Created file [{fileName}]");
                        }
                    }
                }
                WriteSuccess(x.Elapsed);
            }
        }

        public void SyncLanguages(string lang1, string lang2)
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
                        Console.WriteLine("Found " + type + "." + lang1 + ".resx with " + cont1.DataItems.Length + " items");
                        headers1 = cont1.Headers;
                        data1 = new List<DataItem>();
                        data1.AddRange(cont1.DataItems);
                    }

                    if (reader.TryRead(resLang2, out ResourceContainer cont2))
                    {
                        cont2.DataItems = cont2.DataItems ?? new DataItem[0];
                        Console.WriteLine("Found " + type + "." + lang2 + ".resx with " + cont2.DataItems.Length + " items");
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
                    Console.WriteLine($"{lang1} --> {lang2} : Added {i} Entries..");

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
                    Console.WriteLine($"{lang2} --> {lang1} : Added {i} Entries..");
                    reader.Save(resLang1, new ResourceContainer { DataItems = data1.ToArray(), Headers = headers1 });
                    reader.Save(resLang2, new ResourceContainer { DataItems = data2.ToArray(), Headers = headers2 });
                }



                WriteSuccess(x.Elapsed);
            }
        }
    }
}
