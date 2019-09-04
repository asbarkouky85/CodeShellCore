using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Angular;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Services;

namespace CodeShellCore.Moldster.Services.Internal
{
    public class MoldsterService : ConsoleService, IMoldsterService
    {
        readonly WriterService _writer;
        readonly IScriptGenerationService _ts;
        readonly ITemplateProcessingService _html;
        readonly ILocalizationService _loc;
        readonly IDataService _data;
        readonly PathProvider _paths;
        int resultCol = 8;
        public MoldsterService(
            WriterService wt,
            PathProvider paths,
            IScriptGenerationService ts,
            ITemplateProcessingService html,
            ILocalizationService loc,
            IDataService data)
        {
            _writer = wt;
            _ts = ts;
            _html = html;
            _loc = loc;
            _data = data;
            _paths = paths;
        }

        public virtual void RenderModuleDefinition(string modCode, bool lazy)
        {
            RenderMainComponent(modCode);
            _ts.GenerateDomainModule(modCode, "Shared", lazy);
            _ts.GenerateRoutes(modCode, lazy);
            _ts.GenerateModuleDefinition(modCode, lazy);
            _ts.GenerateBootFile(modCode);

            _loc.GenerateJsonFiles(modCode);

        }

        public virtual void RenderDomainModule(string mod, string domain, bool lazy)
        {

            Console.WriteLine();
            Console.Write("Rendering Module ");

            using (ColorSetter.Set(ConsoleColor.Yellow))
                Console.Write(mod);

            Console.WriteLine("----------------------------");
            string moduleName = mod;

            string[] pages = _data.GetDomainPages(mod, domain);

            foreach (var e in pages)
            {
                RenderPage(moduleName, domain, e);
            }
            _ts.GenerateDomainModule(mod, domain, lazy);
            Console.WriteLine();
        }

        public virtual void RenderMainComponent(string mod)
        {
            Console.Write("Writing Main Component for [" + mod + "] : ");
            GotoColumn(resultCol);
            _ts.GenerateMainComponent(mod);
            _html.GenerateMainComponentTemplate(mod);
            Console.WriteLine();
        }

        public virtual void ProcessTemplates(string modCode, string domain = null)
        {
            string[] tem = _data.GetTemplatePaths(modCode, domain);
            foreach (var t in tem)
            {
                _ts.GenerateBaseComponent(t);
                Console.WriteLine();
            }
        }

        public virtual void RenderPage(string moduleName, string domain, string viewPath)
        {
            Console.Write("Writing Component \"" + viewPath + "\" : ");
            GotoColumn(resultCol);

            _ts.GenerateComponent(moduleName, domain, viewPath);
            _html.GenerateComponentTemplate(moduleName, viewPath);

            Console.WriteLine();
        }

        public virtual string GetUIVersion()
        {
            var files = Directory.GetFiles(_paths.UIRoot, "*.csproj");
            if (files.Length > 0)
            {
                string contents = File.ReadAllText(files[0]);
                var ver = contents.FindXmlValue("AssemblyVersion");
                if (ver != null)
                    return ver;
            }
            return "1.0.0.0";
        }

        public virtual void WriteWebpackConfigFiles(bool lazy)
        {
            using (var x = SW.Measure())
            {
                string[] modules = _data.GetModuleCodes();
                string[] act = _data.GetModuleCodes(true);
                _ts.GenerateDevWebPackFiles(modules, act);
                foreach (var t in modules)
                {
                    string[] others = modules.Where(d => d != t).ToArray();
                    _ts.GenerateWebPackFiles(t, others, lazy);
                }
            }

        }

        public virtual void DevelopmentPack()
        {
            string args = $"--max_old_space_size=8138 node_modules/webpack/bin/webpack.js --debug --progress";
            RunCommand(_paths.UIRoot, "node", args);
        }

        public virtual Result ProductionPack(string moduleName, string version)
        {

            string name = $"webpack.{moduleName}.js";
            string file = Path.Combine(_paths.UIRoot, name);

            if (!File.Exists(file))
            {
                return new Result
                {
                    Code = 1,
                    Message = $"File not found '{file}'\n Run WriteWebPackFiles first"
                };
            }
            string args = $"--max_old_space_size=8138 node_modules/webpack/bin/webpack.js --config {name} --env.prod --env.version={version} --debug --progress";
            RunCommand(_paths.UIRoot, "node", args);
            return new Result { Code = 0 };
        }

        public virtual void VendorPack(bool production = false)
        {
            string args = "node_modules/webpack/bin/webpack.js --config webpack.config.vendor.js" + (production ? " --env.prod" : "");
            RunCommand(_paths.UIRoot, "node", args);
        }

        public virtual void AddStaticFiles()
        {
            string path = Path.Combine(_paths.UIRoot, "wwwroot");
            string file = path + "/pkg.zip";
            Utils.CreateFolderForFile(file);
            File.WriteAllBytes(file, Properties.Resources.wwwroot);
            System.IO.Compression.ZipFile.ExtractToDirectory(file, Path.Combine(_paths.UIRoot, "wwwroot"));
            File.Delete(file);

        }

        public virtual void RenderGuid(string module)
        {
            Console.Write("Writing Page \"Guide/Guide\"");

            _html.GenerateGuidTemplate(module);
            _ts.GenerateGuidComponent(module);

            GotoColumn(resultCol);
            WriteSuccess();
            Console.WriteLine();
        }

    }
}
