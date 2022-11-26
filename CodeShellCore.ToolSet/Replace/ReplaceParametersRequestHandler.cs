using CodeShellCore.Cli.Routing;
using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CodeShellCore.Text;
using System.Text.RegularExpressions;

namespace CodeShellCore.ToolSet.Replace
{
    public class ReplaceParametersRequestHandler : CliRequestHandler<ReplaceParametersRequest>
    {
        public ReplaceParametersRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        public override string FunctionDescription => "Replaces parameters in files containing ";

        protected override void Build(ICliRequestBuilder<ReplaceParametersRequest> builder)
        {
            builder.FillProperty(e => e.InputFormat, "input-format", 'f').SetDefault("json");
            builder.FillProperty(e => e.ParameterFile, "source", 's', order: 2);
            builder.FillProperty(e => e.TargetFile, "target", 't', order: 1, isRequired: true);
            builder.FillProperty(e => e.Parameters, "source-string", 'd');
            builder.FillProperty(e => e.ReplacePattern, "pattern", 'p', order: 3).SetDefault("%{}%");
            builder.FillProperty(e => e.UseRegex, "regex", 'r');
        }

        protected override Task<Result> HandleAsync(ReplaceParametersRequest request)
        {
            string jsonParams = request.Parameters;
            if (request.ParameterFile != null)
            {
                if (!File.Exists(request.ParameterFile))
                {
                    throw new FileNotFoundException(request.ParameterFile);
                }
                jsonParams = File.ReadAllText(request.ParameterFile);
            }
            var data = jsonParams.FromJson<Dictionary<string, string>>();

            if (!File.Exists(request.TargetFile))
            {
                Utils.CreateFolderForFile(request.TargetFile);
                File.WriteAllText(request.TargetFile, "");
            }

            var fileContent = File.ReadAllText(request.TargetFile);
            foreach (var par in data)
            {
                var pattern = request.ReplacePattern.Replace("{}", par.Key);
                if (request.UseRegex)
                {
                    var reg = new Regex(pattern);
                    fileContent = reg.Replace(fileContent, par.Value);
                }
                else
                {
                    fileContent = fileContent.Replace(pattern, par.Value);
                }
            }
            File.WriteAllText(request.TargetFile, fileContent);
            return Task.FromResult(new Result());
        }
    }
}
