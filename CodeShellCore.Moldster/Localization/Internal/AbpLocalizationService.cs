using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Text;
using CodeShellCore.Text.Localization;
using CodeShellCore.Text.ResourceReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeShellCore.Moldster.Localization.Internal
{
    public class AbpLocalizationService : LocalizationService, ILocalizationService
    {
        public AbpLocalizationService(IMoldProvider molds, IConfigUnit unit, IPathsService paths, IUIFileNameService names, IOutputWriter output) : base(molds, unit, paths, names, output)
        {
        }

        public override void GenerateJsonFiles(string moduleCode)
        {

        }

        public override void AddLocalizationFiles()
        {

        }

        Dictionary<string, string> GetItems(string type, string locale)
        {
            var ret = new Dictionary<string, string>();
            string resPath = Path.Combine(_paths.LocalizationRoot, locale + ".json");
            if (File.Exists(resPath))
            {
                AbpResourceFile file = File.ReadAllText(resPath).FromJson<AbpResourceFile>();
                ret = file.Texts;
            }
            return ret;
        }

        public override void Import(string type, string lang, List<DataItem> strs, bool suspendOut = false)
        {
            var data = strs.OrderBy(e => e.Name).ToDictionary(e => e.Name, e => e.Value);
            var obj = new AbpResourceFile { Culture = lang, Texts = new Dictionary<string, string>() };
            string resLang1 = Path.Combine(_paths.LocalizationRoot, lang + ".json");
            Utils.CreateFolderForFile(resLang1);
            if (File.Exists(resLang1))
            {
                obj = File.ReadAllText(resLang1).FromJson<AbpResourceFile>();
            }
            foreach (var item in strs)
            {
                if (!obj.Texts.ContainsKey(item.Name))
                {
                    obj.Texts[item.Name] = item.Value?? LangUtils.IdToPhrase(item.Name);
                }
            }
            SaveData(type, lang, obj.Texts);
        }

        public override void InitializeResxFiles()
        {

        }

        public override void SyncAllLanguages()
        {

        }

        public override void SyncLanguages(string lang1, string lang2)
        {

        }

        public override void UpdateFiles(LocalizationDataCollector localization)
        {
            var items = new List<DataItem>();
            string loc = Shell.DefaultCulture.TwoLetterISOLanguageName;
            foreach (var i in localization.Words)
            {
                items.Add(new DataItem { Name = i, Value = "" });
            }
            foreach (var i in localization.Messages)
            {
                items.Add(new DataItem { Name = i, Value = "" });
            }
            foreach (var i in localization.Columns)
            {
                items.Add(new DataItem { Name = i, Value = "" });
            }
            foreach (var i in localization.Pages)
            {
                items.Add(new DataItem { Name = i, Value = "" });
            }
            Import("Pages", loc, items, true);
        }

        public override void FixPages(string tenantCode)
        {
            foreach (var loc in Shell.SupportedLanguages)
            {
                int newItems = 0;
                Out.Write("Fixing for [" + loc + "]");
                Out.GotoColumn(SuccessCol);
                var items = GetItems("Pages", loc);
                Dictionary<string, string> newList = new Dictionary<string, string>();
                List<PageIdentifierDTO> data = _unit.PageRepository.GetDistinctIdentifiers();
                foreach (var item in items)
                {
                    var pageName = item.Key.GetAfterFirst("__");
                    var domainName = item.Key.GetBeforeFirst("__");
                    if (!string.IsNullOrEmpty(item.Value))
                    {
                        var similar = data.Where(e => e.Page == pageName).ToList();
                        foreach (var sim in similar)
                        {
                            newList[sim.Domain + "__" + sim.Page] = item.Value;
                        }
                    }
                    var inDb = data.Any(e => e.Domain == domainName && e.Page == pageName);
                    if (inDb)
                    {
                        newList[item.Key] = item.Value;
                    }
                }

                foreach (var page in data)
                {
                    var key = page.Domain + "__" + page.Page;
                    if (!items.ContainsKey(key) && !newList.ContainsKey(key))
                    {
                        newItems++;
                        newList[key] = "";
                    }
                }
                SaveData("Pages", loc, newList);
                WriteColored("Success [Added : " + newItems + "]", ConsoleColor.Green);
                Out.WriteLine();
            }


        }
        void SaveData(string type, string lang, Dictionary<string, string> lst)
        {
            string resLang1 = Path.Combine(_paths.LocalizationRoot, lang + ".json");

            var data = new AbpResourceFile { Culture = lang, Texts = lst.OrderBy(e => e.Key).ToDictionary(e => e.Key, e => e.Value) };
            File.WriteAllText(resLang1, data.ToJsonIndent());
        }
    }
}
