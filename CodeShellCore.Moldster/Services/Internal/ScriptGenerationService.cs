using CodeShellCore.Cli;
using CodeShellCore.CLI;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Angular.Models;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Models;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CodeShellCore.Moldster.Services.Internal
{
    public abstract class ScriptGenerationService : ConsoleService, IScriptGenerationService
    {
        readonly protected WriterService _writer;
        readonly protected IMoldProvider _molds;
        readonly protected IPathsService _paths;
        private readonly IOutputWriter output;


        public ScriptGenerationService(
            WriterService wt,
            IMoldProvider mold,
            IPathsService paths,
            IOutputWriter output) : base(output)
        {
            _writer = wt;
            _molds = mold;
            _paths = paths;
            this.output = output;
        }

        public void GenerateBootFile(string moduleCode, bool addStyle = false)
        {
            output.Write("Generating boot.ts...  \t\t\t");

            string bootPath = Path.Combine(_paths.UIRoot, moduleCode, "boot.ts");
            string bootTemplate = _molds.BootMold;

            string boot = _writer.FillStringParameters(bootTemplate, new BootTsModel
            {
                Code = moduleCode,
                Style = addStyle ? "import \"./app.scss\"" : ""
            });
            File.WriteAllText(bootPath, boot);

            GotoColumn(SuccessCol);
            WriteSuccess();
            output.WriteLine();
        }

        public void GenerateStyle(string moduleCode, string baseName)
        {
            output.Write("Generating app.scss...  \t\t\t");

            string path = Path.Combine(_paths.UIRoot, moduleCode, "app.scss");
            if (baseName != null)
                File.WriteAllText(path, $"@import \"../Core/{ _paths.CoreAppName}/Styles/{baseName}\";");

            GotoColumn(SuccessCol);
            WriteSuccess();
            output.WriteLine();
        }

        public bool GenerateDataService(string resource, string domain = null)
        {

            //string serviceName = resource + "Service";
            string folder = Path.Combine("Core", _paths.CoreAppName, "Http");

            if (domain != null)
            {
                domain = new Regex("^/").Replace(domain, "").Replace("/", "\\");
                folder = Path.Combine("Core", _paths.CoreAppName, domain, "Http");
            }
            string servicePath = Path.Combine(_paths.UIRoot, folder + "\\" + resource + "Service.ts");
            Utils.CreateFolderForFile(servicePath);
            if (!File.Exists(servicePath))
            {
                string serviceTemplate = _molds.ServiceMold;
                string service = _writer.FillStringParameters(serviceTemplate, new ServiceTsModel { Resource = resource });
                File.WriteAllText(servicePath, service);

                string httpPath = Path.Combine(_paths.UIRoot, folder + ".ts"); ;
                List<string> lst = new List<string>();
                if (File.Exists(httpPath))
                {
                    string[] lines = File.ReadAllLines(httpPath);
                    lst.AddRange(lines);
                }
                lst.Add("export * from \"./Http/" + resource + "Service\";");
                File.WriteAllLines(httpPath, lst);
                return true;
            }
            return false;

        }


        

        public void GenerateGuidComponent(string modl)
        {
            using (var x = SW.Measure())
            {
                using (ColorSetter.Set(ConsoleColor.DarkRed))
                    output.Write(" Ts: ");

                string path = Path.Combine(_paths.UIRoot, modl, "app", "Guide/Guide.ts");

                string scriptPath = path + ".ts";
                var scriptTemplate = _molds.BasicComponent;

                string contents = _writer.FillStringParameters(scriptTemplate, new ComponentTsModel
                {
                    BaseClassLocation = "CodeShell/BaseComponents",
                    BaseClass = "BaseComponent",
                    ComponentName = "Guide",
                    PageId = 0,
                    Domain = "Guide",
                    Resource = null,
                    Selector = "guide",
                    CollectionId = "null"
                });
                Utils.CreateFolderForFile(path);
                File.WriteAllText(scriptPath, contents);
                WriteSuccess();
                output.WriteLine();
            }
        }

        

        public abstract void GenerateBaseComponent(string templatePath);
        public abstract void GenerateComponent(string moduleName, PageRenderDTO viewPath);
        public abstract void GenerateMainComponent(string mod);
        public abstract void GenerateModuleDefinition(string module, bool lazy);
        public abstract void GenerateRoutes(string module, bool lazy);
        public abstract void GenerateDomainModule(string mod, string domain, bool lazy);

        public virtual void GeneratePageCategory(long id)
        {
            
        }

        public virtual void GenerateDomainModuleById(string moduleCode, long? domId, bool lazy = true) { }
    }
}
