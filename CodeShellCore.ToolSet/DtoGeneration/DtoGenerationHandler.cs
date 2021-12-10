using CodeShellCore.Cli.Routing;
using CodeShellCore.Files;
using CodeShellCore.Files.CsProject;
using CodeShellCore.Helpers;
using CodeShellCore.Services;
using CodeShellCore.Text;
using CodeShellCore.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.DtoGeneration
{
    public class DtoGenerationHandler : CliRequestHandler<DtoGenerationRequest>
    {
        private string NameSpace { get; set; }
        private WriterService _writer;

        const string DtoTemplate = @"using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Text.Localization;

namespace %Namespace%
{
    [EntityName(""%EntityClassName%"")]
    public class %DtoClassName%
    {
%Properties%
    }
}
";
        public DtoGenerationHandler(IServiceProvider provider) : base(provider)
        {
            _writer = new WriterService();
        }

        protected override void Build(ICliRequestBuilder<DtoGenerationRequest> builder)
        {
            builder.FillProperty(e => e.WorkingDirectory, "folder", 'd', 1, true);
            builder.FillProperty(e => e.EntityProject, "project", 'p', 2, true);
            builder.FillProperty(e => e.EntityType, "entity", 'e', 3, true);
            builder.FillProperty(e => e.Options, "options", 't', 4, true);

            builder.FillProperty(e => e.OutputProject, "output", 'o');
            builder.FillProperty(e => e.NoBuild, "no-build");
        }

        FileText GetDtoContents(Type classType, string dtoType, string defaultNamespace = null)
        {

            var props = classType.GetProperties().Where(e => e.PropertyType.IsValueType);
            var dto = new DtoTemplateDto
            {
                DtoClassName = classType.Name + dtoType + "Dto",
                EntityClassName = classType.Name,
                Namespace = classType.Namespace,
                Properties = "\t\t#region Generated"
            };
            defaultNamespace = defaultNamespace ?? classType.Assembly.GetName().Name;
            WriteFileOperation("Generating", dto.DtoClassName);
            Regex reg = new Regex("^" + defaultNamespace);
            var folder = reg.Replace(classType.Namespace, "").Replace(".", "\\") ?? "";
            var path = Path.Combine(folder, dto.DtoClassName + ".cs");
            foreach (var prop in props)
            {
                dto.Properties += "\n\t\tpublic " + prop.PropertyType.ToPropertyTypeString() + " " + prop.Name + " { get; set;}";
            }
            dto.Properties += "\n\t\t#end region";
            return new FileText { Contents = _writer.FillStringParameters(DtoTemplate, dto), Path = path };
        }

        protected override Task<Result> HandleAsync(DtoGenerationRequest request)
        {
            if (!CsProjectFile.TryFindCsProj(request.WorkingDirectory, request.EntityProject, out CsProjectFile csProj))
            {
                throw new Exception("Project not found " + request.EntityProject);
            }

            var assemblyName = csProj.GetAssemblyName();
            var tagetFw = csProj.GetValueByTagName("TargetFramework");
            var buildOutputPath = csProj.GetValueByTagName("OutputPath");

            var directory = csProj.Folder;
            var genOutputPath = directory;

            if (request.OutputProject != null)
            {
                if (CsProjectFile.TryFindCsProj(request.WorkingDirectory, request.OutputProject, out CsProjectFile outProj))
                {
                    genOutputPath = outProj.Folder;
                }
                else
                {
                    throw new Exception("Project not found " + outProj);
                }
            }

            if (!request.NoBuild)
            {
                WriteFileOperation("Building", request.EntityProject);
                Utils.RunCmdCommand("dotnet build", directory);
            }

            var files = Directory.GetFiles(directory, "*" + assemblyName + ".dll", SearchOption.AllDirectories).ToList();
            files = files.Where(e => (e.Contains("bin") || (buildOutputPath != null && e.Contains(buildOutputPath))) && e.Contains(tagetFw)).ToList();

            string[] dtoTypes = new[] { "Create", "Update", "List" };
            var entityName = request.EntityType.GetAfterLast(".");
            if (files.Any())
            {
                var assembly = Assembly.LoadFrom(files[0]);
                var type = assembly.GetType(request.EntityType);
                if (type == null)
                {
                    type = assembly.GetType(csProj.DefaultNamespace + "." + request.EntityType);
                }
                if (type != null)
                {
                    foreach (var t in dtoTypes)
                    {
                        var content = GetDtoContents(type, t, csProj.DefaultNamespace);
                        var path = Path.Combine(genOutputPath, content.Path);
                        Utils.CreateFolderForFile(path);
                        File.WriteAllText(path, content.Contents);
                    }
                }
                else
                {
                    throw new Exception("Type not found " + assemblyName + "." + request.EntityType + " try providing a fully qualified class name");
                }
                //var types = assembly.GetTypes();
            }
            else
            {
                throw new Exception("Output not found try building the project");
            }
            return Task.FromResult(new Result());
        }
    }
}
