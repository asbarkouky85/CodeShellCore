using CodeShellCore.Cli;
using CodeShellCore.Files.CsProject;
using CodeShellCore.Helpers;
using CodeShellCore.ToolSet.Modularity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeShellCore.Text;
using CodeShellCore.Services;
using System.Reflection;
using CodeShellCore.Types;
using CodeShellCore.Text.Localization;
using System.Text.RegularExpressions;

namespace CodeShellCore.Modularity
{
    public class ModuleClassGenerationService : StandaloneConsoleService, IModuleClassGenerationService
    {
        public ModuleClassGenerationService(IServiceProvider provider) : base(provider)
        {
        }

        private string _toClassName(string projectName)
        {
            var cls = (string.Join("", projectName.Split('.')) + "Module").ApplyClassNameConvension();
            cls = cls.Replace("CodeShellCore", "CodeShell");
            return cls;
        }

        public Task<Result> Generate(GenerateModuleClassesRequest request)
        {
            var writer = new WriterService();
            var template = Assembly.GetCallingAssembly().GetEmbeddedResourceAsString("module_class_template");
            var files = Directory.GetFiles(request.SolutionFolder, "*.csproj", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var proj = new CsProjectFile(file, new CsProjectFileReader());
                var fileInfo = new FileInfo(file);
                var model = new GenerateModuleClassesReplaceModel();
                model.ClassName = _toClassName(proj.ProjectName);
                model.Namespace = proj.DefaultNamespace;
                var generatedFilePath = Path.Combine(fileInfo.Directory.FullName, model.ClassName + ".cs");
                WriteFileOperation("Generating", model.ClassName, false);
                if (Directory.GetFiles(fileInfo.Directory.FullName, "*Module.cs", SearchOption.TopDirectoryOnly).Any())
                {
                    GotoColumn(10);
                    WriteColored("Exists", ConsoleColor.DarkGray);
                    Out.WriteLine();
                    continue;
                }
                List<string> usings = new List<string>();
                List<string> dependencies = new List<string>();
                foreach (var reference in proj.ProjectReferences)
                {
                    var refereceProjectPath = Path.Combine(fileInfo.Directory.FullName, reference);
                    if (File.Exists(refereceProjectPath))
                    {
                        var refereceProject = new CsProjectFile(refereceProjectPath, new CsProjectFileReader());
                        if (!usings.Any(e => e.Contains(refereceProject.DefaultNamespace + ";")) &&
                            refereceProject.DefaultNamespace != proj.DefaultNamespace)
                        {
                            usings.Add($"using {refereceProject.DefaultNamespace};");
                        }
                        dependencies.Add($"typeof({_toClassName(refereceProject.ProjectName)})");
                    }
                }
                model.Usings = string.Join("\n", usings);
                model.Dependencies = string.Join(",\n\t\t", dependencies);
                var generatedFile = writer.FillStringParameters(template, model);
                File.WriteAllText(generatedFilePath, generatedFile);
                WriteSuccess(column: 10);
                Out.WriteLine();
            }
            return Task.FromResult(new Result());
        }
    }
}
