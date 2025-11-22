using CodeShellCore.CliDispatch.Parsing;
using CodeShellCore.CliDispatch.Routing;
using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.Localization
{
    public class AbpExtractLocalizationKeysHandler : CliRequestHandler<AbpExtractLocalizationKeysRequest>
    {
        private record LocalizationItem(string Resource, string Key);
        public AbpExtractLocalizationKeysHandler(IServiceProvider provider) : base(provider)
        {
        }

        public override string FunctionDescription => "";

        protected override void Build(ICliRequestBuilder<AbpExtractLocalizationKeysRequest> builder)
        {
            builder.Property(e => e.FrontEndFolder, "f", "front", 1, true);
            builder.Property(e => e.BackendFolder, "b", "back", 2, true);
            builder.Property(e => e.DefaultResourceName, "r", "default", 3, true);
        }

        protected override async Task<Result> HandleAsync(AbpExtractLocalizationKeysRequest request, CancellationToken token)
        {
            var files = Directory.GetFiles(request.FrontEndFolder, "*.html", SearchOption.AllDirectories);
            files = files.Where(e => !e.Contains("node_modules")).ToArray();
            var regex = new Regex(@"'([a-zA-Z0-9_\-\:]+)' ?\| ?abpLocalization");
            var keys = new List<LocalizationItem>();
            foreach (var file in files)
            {
                var lines = await File.ReadAllLinesAsync(file);

                foreach (var line in lines)
                {

                    if (regex.IsMatch(line))
                    {
                        var matches = regex.Matches(line);
                        foreach (Match group in matches)
                        {
                            var split = group.Groups[1].Value.Split("::");
                            if (split.Length > 1)
                            {
                                keys.Add(new LocalizationItem(split[0], split[1]));
                            }
                        }
                    }
                }
            }
            keys = keys.ToList().Distinct().ToList();

            var mainResourceFile = Directory.GetFiles(request.BackendFolder, request.DefaultResourceName + "Resource.cs", SearchOption.AllDirectories);
            string mainResourceFolder = null;
            if (mainResourceFile.Any())
            {
                mainResourceFolder = Path.Combine(new FileInfo(mainResourceFile[0]).DirectoryName, request.DefaultResourceName);
                var writer = new AbpResourceFileWriter(mainResourceFolder);
                await writer.AppendKeys(keys.Where(e => e.Resource == "").Select(e => e.Key).ToList());
            }
            return new Result();
        }
    }
}
