using System;
using System.IO;

using CodeShellCore.Cli;
using CodeShellCore.CLI;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Services.Internal;
using CodeShellCore.Services;

namespace CodeShellCore.Moldster.Services.Json
{
    public class JsonTemplateProcessingService : TemplateProcessingService
    {
        private readonly IJsonConfigProvider _data;
        private readonly IOutputWriter output;

        public JsonTemplateProcessingService(
            IViewsService service,
            IPathsService paths,
            IJsonConfigProvider data,
            IOutputWriter output) : base(service, paths, output)
        {
            _data = data;
            this.output = output;
        }

        public override void GenerateComponentTemplate(string moduleName, PageRenderDTO dto)
        {
            var p = _data.GetPageConfig(moduleName, dto.ViewPath);
            using (var x = SW.Measure())
            {
                GotoColumn(6);
                using (ColorSetter.Set(ConsoleColor.DarkYellow))
                    output.Write("\tHtml : ");

                string template = _views.GetPage(new PageAcquisitorDTO
                {
                    ViewPath = p.ViewPath,
                    ModuleCode = moduleName
                });
                string path = Path.Combine(_paths.UIRoot, moduleName, "app", p.ComponentPath);
                string templatePath = path + ".html";

                Utils.CreateFolderForFile(templatePath);

                File.WriteAllText(templatePath, template);

                WriteSuccess(x.Elapsed);
            }
        }

        public override void GenerateMainComponentTemplate(string moduleCode)
        {
            NgModule mod = _data.GetNgModule(moduleCode);

            string contents = _views.GetMainComponent(mod.MainComponentBase);
            string filePath = Path.Combine(_paths.UIRoot, mod.Name, "app", mod.MainComponent + ".html");

            Utils.CreateFolderForFile(filePath);
            File.WriteAllText(filePath, contents);
        }
    }
}
