using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Db.Services;
using CodeShellCore.Moldster.Models;
using CodeShellCore.Services;
using CodeShellCore.Text;
using CodeShellCore.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeShellCore.Moldster.Services.Internal
{
    public abstract class ScriptGenerationService : ConsoleService, IScriptGenerationService
    {
        readonly protected WriterService _writer;
        readonly protected IMoldProvider _molds;
        readonly protected PathProvider _paths;
        readonly protected IMappedEnumerations _enums;

        protected const int resultcol = 8;
        public ScriptGenerationService(
            WriterService wt,
            IMoldProvider mold,
            PathProvider paths,
            IMappedEnumerations enums)
        {
            _writer = wt;
            _molds = mold;
            _paths = paths;
            _enums = enums;
        }

        public void GenerateBootFile(string moduleCode)
        {
            Console.Write("Generating boot.ts...  \t\t\t");

            string bootPath = Path.Combine(_paths.UIRoot, moduleCode, "boot.ts");
            string bootTemplate = _molds.BootMold;

            string boot = _writer.FillStringParameters(bootTemplate, new ModuleTsModel { Code = moduleCode });
            File.WriteAllText(bootPath, boot);

            GotoColumn(resultcol);
            WriteSuccess();
            Console.WriteLine();
        }

        public bool GenerateDataService(string resource, string domain = null)
        {

            string serviceName = resource + "Service";
            string servicePath = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, "Http\\" + serviceName + ".ts");
            Utils.CreateFolderForFile(servicePath);
            if (!File.Exists(servicePath))
            {
                string serviceTemplate = _molds.ServiceMold;
                string service = _writer.FillStringParameters(serviceTemplate, new ServiceTsModel { Resource = resource });
                File.WriteAllText(servicePath, service);

                string httpPath = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, "Http.ts");
                List<string> lst = new List<string>();
                if (File.Exists(httpPath))
                {
                    string[] lines = File.ReadAllLines(httpPath);
                    lst.AddRange(lines);
                }
                lst.Add("export * from \"./Http/" + serviceName + "\";");
                File.WriteAllLines(httpPath, lst);
                return true;
            }
            return false;

        }


        public void GenerateDevWebPackFiles(IEnumerable<string> modules, IEnumerable<string> activeMods = null)
        {
            IEnumerable<string> apps = modules;
            activeMods = activeMods ?? apps;
            string packTemplate = _molds.DevWebpackConfigMold;
            Console.Write("Generating webpack.config.js\t");
            WebPackModel mod = new WebPackModel();
            string sep = "";
            foreach (string app in activeMods)
            {
                mod.Tenants += sep + $"\"{app}\" : \"./{app}/boot.ts\",\r";
                sep = "\t\t\t";
            }
            string contents = _writer.FillStringParameters(packTemplate, mod);
            string packPath = Path.Combine(_paths.UIRoot, "webpack.config.js");
            File.WriteAllText(packPath, contents);
            WriteSuccess(null, resultcol);
            Console.WriteLine();

        }

        public void GenerateWebPackFiles(string code, IEnumerable<string> others, bool lazy)
        {
            string packTemplate = _molds.ProWebpackConfigMold;
            string jsonTemplate = _molds.ModuleTsConfigMold;

            WebPackModel mod = new WebPackModel
            {
                Code = code,
                Tenants = "",
                Lazy = lazy ? "" : ""
            };

            foreach (var t in others)
                mod.Tenants += $"\"{t}\",\r";

            string packContents = _writer.FillStringParameters(packTemplate, mod);
            string jsonContents = _writer.FillStringParameters(jsonTemplate, mod);

            string packPath = $"webpack.{code}.js";
            string jsonPath = $"webpack.{code}.js.tsc";

            Console.Write("Generating " + packPath + "\t");
            File.WriteAllText(Path.Combine(_paths.UIRoot, packPath), packContents);
            WriteSuccess(null, resultcol);
            Console.WriteLine();

            Console.Write("Generating " + jsonPath + "\t");
            File.WriteAllText(Path.Combine(_paths.UIRoot, jsonPath), jsonContents);
            WriteSuccess(null, resultcol);
            Console.WriteLine();

        }

        public void GenerateGuidComponent(string modl)
        {
            using (var x = SW.Measure())
            {
                Console.Write("Rendering " + modl + " Guide..");

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
                Console.WriteLine();
            }
        }

        public string MapEnum<T>()
        {
            //if (!typeof(T).IsAssignableFrom(typeof(Enum)))
            //{
            //    return "";
            //}
            var s = Enum.GetValues(typeof(T));
            var ty = Enum.GetUnderlyingType(typeof(T));
            var ret = "export enum " + typeof(T).Name + " {\n";
            foreach (var v in s)
            {
                ret += "\t" + v + " = " + Convert.ChangeType(v, ty) + " ,\n";
            }
            ret += "};\n\n";
            return ret;
        }

        public string MapEntity<T>()
        {
            var lst = typeof(T).GetValueProperties();
            string cl = "export class " + typeof(T).Name + " {";
            foreach (var prop in lst)
            {
                cl += "\n\t" +
                    prop.Name.LCFirst() +
                    (prop.PropertyType.IsNullable() ? "?:" : ":") + " " +
                    GetTsType(prop.PropertyType) +
                    GetInitializer(prop.PropertyType) + ";";
            }
            cl += "\n}";
            Console.WriteLine("Type : " + typeof(T).Name);
            return cl;
        }

        public string GetTsType(Type t)
        {
            if (t == typeof(string))
                return "string";

            if (t.RealType() == typeof(bool))
                return "boolean";

            if (t.RealType() == typeof(DateTime))
                return "Date";

            if (t.RealType().IsDecimalType() || t.RealType().IsIntgerType())
                return "number";


            return "any";
        }

        public string GetInitializer(Type t)
        {
            if (t.IsNullable())
                return "";
            if (t == typeof(string))
                return " = \"\" ";

            if (t.RealType() == typeof(bool))
                return " = false";

            if (t.RealType().IsDecimalType() || t.RealType().IsIntgerType())
                return " = 0";

            if (t.RealType() == typeof(DateTime))
                return " = new Date() ";
            return " = {}";
        }

        public void GenerateEnums()
        {
            if (_enums.IsActive)
            {
                var pat = Path.Combine(_paths.UIRoot, _enums.FilePath);
                File.WriteAllText(pat, _enums.EnumsGetter(this));
            }
        }

        public abstract void GenerateBaseComponent(string templatePath);

        public abstract void GenerateComponent(string moduleName, string domain, string viewPath);

        public abstract void GenerateMainComponent(string mod);

        public abstract void GenerateModuleDefinition(string module, bool lazy);

        public abstract void GenerateRoutes(string module, bool lazy);

        public void GenerateEnvironment()
        {
            string path = Path.Combine(_paths.UIRoot, "declarations.d.ts");

            var sql = Properties.Resources.Creation;
            File.WriteAllText(Path.Combine(_paths.ConfigRoot, "Creation.sql"), sql);
            Console.WriteLine("Adding file [Creation.sql] to " + _paths.ConfigRoot);

            if (!File.Exists(path))
            {
                Console.WriteLine("Adding file [declarations.d.ts]");
                File.WriteAllText(path, Properties.Resources.declarations_d);
            }

            path = Path.Combine(_paths.UIRoot, "package.json");
            if (!File.Exists(path))
            {
                Console.WriteLine("Adding file [package.json]");
                File.WriteAllText(path, Properties.Resources.package_json);
            }

            path = Path.Combine(_paths.UIRoot, "tsconfig.json");
            if (!File.Exists(path))
            {
                Console.WriteLine("Adding file [tsconfig.json]");
                File.WriteAllText(path, Properties.Resources.tsconfig_json);
            }

            path = Path.Combine(_paths.UIRoot, "WebPackSharedConfig.js");
            if (!File.Exists(path))
            {
                Console.WriteLine("Adding file [WebPackSharedConfig.js]");
                File.WriteAllText(path, Properties.Resources.WebPackSharedConfig_js);
            }

            path = Path.Combine(_paths.UIRoot, "webpack.config.vendor.js");
            if (!File.Exists(path))
            {
                Console.WriteLine("Adding file [webpack.config.vendor.js]");
                File.WriteAllText(path, Properties.Resources.webpack_config_vendor_js);
            }
            path = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, "ServerConfig.ts");
            Utils.CreateFolderForFile(path);
            if (!File.Exists(path))
            {
                Console.WriteLine("Adding file [ServerConfig.ts]");
                File.WriteAllText(path, Properties.Resources.ServerConfig_ts);
            }
            path = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, "AppComponentBase.ts");
            Utils.CreateFolderForFile(path);
            if (!File.Exists(path))
            {
                Console.WriteLine("Adding file [AppComponentBase.ts]");
                File.WriteAllText(path, Properties.Resources.AppComponentBase_ts);
            }

            path = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, "Main/Login.ts");
            Utils.CreateFolderForFile(path);
            if (!File.Exists(path))
            {
                Console.WriteLine("Adding file [Login.ts]");
                File.WriteAllText(path, Properties.Resources.Login_ts);
            }

            path = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, "Main/Login.html");
            Utils.CreateFolderForFile(path);
            if (!File.Exists(path))
            {
                Console.WriteLine("Adding file [Login.html]");
                File.WriteAllText(path, Properties.Resources.Login_html);
            }
            path = Path.Combine(_paths.UIRoot, "Core", _paths.CoreAppName, _paths.CoreAppName + "BaseModule.ts");
            if (!File.Exists(path))
            {
                Console.WriteLine("Adding file [" + _paths.CoreAppName + "BaseModule.ts]");
                string content = _writer.FillStringParameters(_molds.BaseModuleMold, new DomainTsModel { Name = _paths.CoreAppName+"Base" });
                File.WriteAllText(path, content);
            }

            path = Path.Combine(_paths.UIRoot, "appsettings.development.json");
            if (!File.Exists(path))
            {
                Console.WriteLine("Adding file [appsettings.development.json]");
                File.WriteAllText(path, Properties.Resources.appsettings_json);
            }

            path = Path.Combine(_paths.ConfigRoot, "Views/AppComponent.cshtml");
            if (!File.Exists(path))
            {
                Console.WriteLine("Adding file [AppComponent.cshtml]");
                File.WriteAllText(path, Properties.Resources.AppComponent_cshtml);

            }

            path = Path.Combine(_paths.UIRoot, "Pages/Index.cshtml");
            if (!File.Exists(path))
            {
                Console.WriteLine("Adding file [Index.cshtml]");
                File.WriteAllText(path, Properties.Resources.Index_cshtml);

            }

        }

        

        public abstract void GenerateDomainModule(string mod, string domain, bool lazy);
    }
}
