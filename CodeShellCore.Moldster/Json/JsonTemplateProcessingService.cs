using System;
using System.IO;

using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Services.Internal;
using CodeShellCore.Services;

namespace CodeShellCore.Moldster.Json
{
    public class JsonTemplateProcessingService : TemplateProcessingService
    {
        private readonly IJsonConfigProvider _data;

        public JsonTemplateProcessingService(
            IViewsService service,
            PathProvider paths,
            WriterService wt,
            IJsonConfigProvider data) : base(service, paths, wt)
        {
            _data = data;
        }

        public override void GenerateComponentTemplate(string moduleName, string viewPath)
        {
            var p = _data.GetPageConfig(moduleName, viewPath);
            using (var x = SW.Measure())
            {
                GotoColumn(6);
                using (ColorSetter.Set(ConsoleColor.DarkYellow))
                    Console.Write("\tHtml : ");

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
