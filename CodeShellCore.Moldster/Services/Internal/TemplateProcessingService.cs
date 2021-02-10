using CodeShellCore.Cli;
using CodeShellCore.CLI;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeShellCore.Moldster.Services.Internal
{
    public abstract class TemplateProcessingService : ConsoleService, ITemplateProcessingService
    {
        protected readonly IViewsService _views;
        protected readonly IPathsService _paths;
        public TemplateProcessingService(IViewsService service, IPathsService paths, IOutputWriter output) : base(output)
        {
            _views = service;
            _paths = paths;
        }

        public abstract void GenerateComponentTemplate(string moduleName, PageRenderDTO viewPath);

        public void GenerateGuidTemplate(string moduleCode)
        {
            using (Out.Set(ConsoleColor.DarkYellow))
                Out.Write(" Html: ");
            var contents = _views.GetGuide(moduleCode);
            string path = Path.Combine(_paths.UIRoot, moduleCode, "app", "Guide/Guide.html"); ;

            Utils.CreateFolderForFile(path);
            File.WriteAllText(path, contents);
            WriteSuccess();
        }



        public abstract void GenerateMainComponentTemplate(string moduleCode);



    }
}
