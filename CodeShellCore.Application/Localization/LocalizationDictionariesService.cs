using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Services;
using CodeShellCore.Text.Localization;
using CodeShellCore.Text.ResourceReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Localization
{
    public class LocalizationDictionariesService : StandaloneConsoleService, ILocalizationDictionariesService
    {
        ILocaleTextProvider _textProvider => Store.GetRequiredService<ILocaleTextProvider>();

        public LocalizationDictionariesService(IServiceProvider provider) : base(provider)
        {
        }

        public Task<LocalizationDictionaryDto> Get(LocalizationDictionariesRequestDto dto)
        {
            return Task.Run(() =>
            {
                var cult = dto.Lang ?? Language.Culture.TwoLetterISOLanguageName;
                return new LocalizationDictionaryDto
                {
                    Columns = _textProvider.GetAllColumns(cult),
                    Messages = _textProvider.GetAllMessages(cult),
                    Pages = _textProvider.GetAllPages(cult),
                    Words = _textProvider.GetAllWords(cult)
                };
            });
        }

        public virtual async Task SyncAllLanguages(string folder)
        {
            var doneLangs = new List<string>();
            foreach (var s in Shell.SupportedLanguages)
            {
                foreach (var s2 in Shell.SupportedLanguages)
                {
                    if (s != s2 && !doneLangs.Contains(s2))
                        await SyncLanguages(folder, s, s2);
                }
                doneLangs.Add(s);
            }
        }

        public virtual Task SyncLanguages(string folder, string lang1, string lang2)
        {
            return Task.Run(() =>
            {
                using (var x = SW.Measure())
                {
                    string[] types = new string[] { "Columns", "Words", "Pages", "Messages" };

                    foreach (string type in types)
                    {
                        string resLang1 = Path.Combine(folder, "Localization", type + "." + lang1 + ".resx");
                        string resLang2 = Path.Combine(folder, "Localization", type + "." + lang2 + ".resx");

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
                        reader.Save(resLang1, new ResourceContainer { DataItems = data1.OrderBy(e => e.Name).ToArray(), Headers = headers1 });
                        reader.Save(resLang2, new ResourceContainer { DataItems = data2.OrderBy(e => e.Name).ToArray(), Headers = headers2 });
                    }

                    WriteSuccess(x.Elapsed);
                }
            });
        }
    }
}
