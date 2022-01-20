using CodeShellCore.Cli.Routing;
using CodeShellCore.Helpers;
using CodeShellCore.Text.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.Localization
{
    public class AbpSyncLanguagesRequestHandler : CliRequestHandler<AbpSyncLanguagesRequest>
    {
        public override string FunctionDescription => "Sycnronizes abp localization .json files";

        public AbpSyncLanguagesRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        protected override void Build(ICliRequestBuilder<AbpSyncLanguagesRequest> builder)
        {
            builder.FillProperty(e => e.MainDirectory, "folder", 'd', 2, isRequired: true);
            builder.FillProperty(e => e.LocalizationRoot, "project", 'p', 1, isRequired: true);
            builder.FillProperty(e => e.Lang1, "lang1, 's'");
            builder.FillProperty(e => e.Lang2, "lang2", 't');
        }

        protected override Task<Result> HandleAsync(AbpSyncLanguagesRequest request)
        {
            var lang1 = request.Lang1 ?? "ar";
            var lang2 = request.Lang2 ?? "en";
            var root = Path.Combine(request.MainDirectory, request.LocalizationRoot);
            string resLang1 = Path.Combine(root, lang1 + ".json");
            string resLang2 = Path.Combine(root, lang2 + ".json");

            var data1 = GetItems(root, "", lang1);
            var data2 = GetItems(root, "", lang2);

            int i = 0;
            foreach (var item in data1)
            {
                if (!data2.Any(d => d.Key == item.Key))
                {
                    data2[item.Key] = LangUtils.IdToPhrase(item.Key);
                    i++;
                }

            }
            Console.WriteLine($"{lang1} --> {lang2} : Added {i} Entries..");

            i = 0;
            foreach (var item in data2)
            {
                if (!data1.Any(d => d.Key == item.Key))
                {
                    data1[item.Key] = LangUtils.IdToPhrase(item.Key);
                    i++;
                }
            }
            Console.WriteLine($"{lang2} --> {lang1} : Added {i} Entries..");
            SaveData(root, "", lang1, data1.OrderBy(e => e.Key).ToDictionary(e => e.Key, e => e.Value));
            SaveData(root, "", lang2, data2.OrderBy(e => e.Key).ToDictionary(e => e.Key, e => e.Value));
            return Task.FromResult(new Result());
        }

        private Dictionary<string, string> GetItems(string root, string type, string locale)
        {
            var ret = new Dictionary<string, string>();
            string resPath = Path.Combine(root, locale + ".json");
            if (File.Exists(resPath))
            {
                var txt = File.ReadAllText(resPath);

                AbpResourceFile file = JsonConvert.DeserializeObject<AbpResourceFile>(txt);
                ret = file.Texts;
            }
            return ret;
        }

        void SaveData(string root, string type, string lang, Dictionary<string, string> lst)
        {
            string resLang1 = Path.Combine(root, lang + ".json");

            var data = new AbpResourceFile { Culture = lang, Texts = lst.OrderBy(e => e.Key).ToDictionary(e => e.Key, e => e.Value) };
            File.WriteAllText(resLang1, JsonConvert.SerializeObject(data, Formatting.Indented));
        }

        public string[] GetHelp()
        {
            return new string[]
            {
                "[json path] [language 1] [language 2]",
                "Synchronizing keys between languages"
            };
        }
    }
}
