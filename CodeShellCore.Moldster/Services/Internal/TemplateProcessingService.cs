using CodeShellCore.Cli;
using CodeShellCore.Helpers;
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
        protected readonly WriterService Writer;
        protected readonly PathProvider _paths;
        public TemplateProcessingService(IViewsService service, PathProvider paths, WriterService wt)
        {
            _views = service;
            Writer = wt;
            _paths = paths;
        }

        public abstract void GenerateComponentTemplate(string moduleName, string viewPath);

        public void GenerateGuidTemplate(string moduleCode)
        {
            var contents = _views.GetGuide(moduleCode);
            string path = Path.Combine(_paths.UIRoot, moduleCode, "app", "Guide/Guide.html"); ;

            Utils.CreateFolderForFile(path);
            File.WriteAllText(path, contents);
        }

        

        public abstract void GenerateMainComponentTemplate(string moduleCode);



    }
}
