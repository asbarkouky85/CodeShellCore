using CodeShellCore.CliDispatch.Parsing;
using CodeShellCore.CliDispatch.Routing;
using CodeShellCore.Files.CsProject;
using CodeShellCore.Helpers;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CodeShellCore.Types;
using System.Threading;
using CodeShellCore.Files;

namespace CodeShellCore.ToolSet.Analyzer
{
    public class AnalyzerRequestHandler : CliRequestHandler<AnalyzerRequest>
    {
        public AnalyzerRequestHandler(IServiceProvider provider) : base(provider)
        {
        }

        public override string FunctionDescription => "Read class names from dll";

        protected override void Build(ICliRequestBuilder<AnalyzerRequest> builder)
        {
            builder.Property(e => e.Folder, "folder", "d", 1, true);
            builder.Property(e => e.TargetFramework, "target", "tf").SetDefault("net8.0");
            builder.Property(e => e.CentralVersions, "centralized", "cv").SetDefault(false);
        }

        protected override Task<Result> HandleAsync(AnalyzerRequest request, CancellationToken token)
        {
            var files = Directory.GetFiles(request.Folder, "*.csproj", SearchOption.AllDirectories);
            var projectName = "Dependency.Analyzer";
            var projectFolder = Path.Combine(request.Folder, projectName);

            var projectArchive = typeof(AnalyzerRequestHandler).Assembly.GetEmbeddedResourceBytes("dependency_analyzer_csproj");
            var tempPath = Path.Combine("./_proj.zip");
            File.WriteAllBytes(tempPath, projectArchive);

            FileUtils.DecompressDirectory(tempPath, projectFolder, new DecompressOptions
            {
                KeepFiles = new[] {
                    "DependencyAnalyzerOptions.cs",
                    "DependencyClassifyService.cs",
                    "module_definition_template"
                }
            });

            var deps = new List<string>();
            var assemblies = new List<string>();
            foreach (var file in files)
            {
                deps.Add(file.Replace(request.Folder, "..\\"));
                var proj = new CsProjectFile(file, new CsProjectFileReader());
                assemblies.Add("\"" + proj.GetAssemblyName() + "\"");
            }

            GetService<ICsProjectCreateService>().Create(projectName, request.TargetFramework, deps, projectFolder,request.CentralVersions);
            var writer = new WriterService();
            var programTemplate = typeof(AnalyzerRequestHandler).Assembly.GetEmbeddedResourceAsString("dependency_analyzer_program.template");
            var programCode = writer.FillStringParameters(programTemplate, new
            {
                Assemblies = string.Join(",\r\n\t\t", assemblies),
                Directory = request.Folder.Replace("\\", "\\\\"),
            });


            File.WriteAllText(Path.Combine(projectFolder, "Program.cs"), programCode);
            return Task.FromResult(new Result(0));
        }
    }
}
