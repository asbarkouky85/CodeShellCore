using CodeShellCore.Cli.Routing;
using CodeShellCore.Files.CsProject;
using CodeShellCore.Helpers;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.DtoGeneration
{
    public class DtoGenerationHandler : CliRequestHandler<DtoGenerationRequest>
    {
        private string NameSpace { get; set; }

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
        }

        protected override void Build(ICliRequestBuilder<DtoGenerationRequest> builder)
        {
            builder.FillProperty(e => e.ProjectFile, "project", 'd', 1, true);
            builder.FillProperty(e => e.EntityType, "entity", 'e', 2, true);
            builder.FillProperty(e => e.Options, "options", 'o', 3, true);
            builder.FillProperty(e => e.NoBuild, "no-build");
        }

        void WriteDto(string type)
        {

        }

        protected override Task<Result> HandleAsync(DtoGenerationRequest request)
        {
            var csProj = new CsProjectFile(request.ProjectFile, new CsProjectFileReader());
            var assemblyName = csProj.GetAssemblyName();
            var tagetFw = csProj.GetValueByTagName("TargetFramework");
            var directory = Path.GetDirectoryName(request.ProjectFile);
            var files = Directory.GetFiles(directory, "*" + assemblyName + ".dll", SearchOption.AllDirectories).ToList();
            files = files.Where(e => e.Contains("bin") && e.Contains("Debug") && e.Contains(tagetFw)).ToList();
            var dto = new DtoTemplateDto
            {
                DtoClassName = ""
            };
            string[] dtoTypes = new[] { "Create", "Update", "List" };
            var entityName = request.EntityType.GetAfterLast(".");
            if (files.Any())
            {
                var assembly = Assembly.LoadFrom(files[0]);
                var type = assembly.GetType(request.EntityType);
                if (type == null)
                {
                    assembly.GetType(assemblyName + request.EntityType);
                }
                var props = type.GetProperties();

                foreach (var prop in props)
                {

                }
                //var types = assembly.GetTypes();
            }
            return Task.FromResult(new Result());
        }
    }
}
