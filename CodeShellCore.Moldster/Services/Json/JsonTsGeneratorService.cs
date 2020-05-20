using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Services;
using CodeShellCore.Text;

using CodeShellCore.Moldster.Models;
using CodeShellCore.Moldster.Angular.Models;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Services.Internal;
using CodeShellCore.CLI;

namespace CodeShellCore.Moldster.Services.Json
{
    public class JsonTsGeneratorService : ScriptGenerationService, IScriptGenerationService
    {
        readonly IJsonConfigProvider _data;
        private readonly IOutputWriter output;
        readonly int SuccessColumn = 7;
        public JsonTsGeneratorService(
            WriterService wt,
            IMoldProvider mold,
            IJsonConfigProvider data,
            IPathsService paths,
            IOutputWriter output) : base(wt, mold, paths,output)
        {
            _data = data;
            this.output = output;
        }

        public JsonTsGeneratorService(
            WriterService wt, 
            IMoldProvider mold,
            IPathsService paths,
            IOutputWriter wtt) : base(wt, mold, paths,wtt)
        {
        }

        public override void GenerateMainComponent(string module)
        {
            NgModule mod = _data.GetNgModule(module);
            string temp = _molds.MainComponentMold;
            var model = new AppComponentModel
            {
                Name = mod.MainComponent,
                BaseComponentName = mod.MainComponentBase.GetAfterLast("/") + "Base",
                BaseComponentPath = "Base/" + mod.MainComponentBase + "Base"
            };
            string contents = _writer.FillStringParameters(temp, model);
            string path = Path.Combine(_paths.UIRoot, mod.Name, "app", mod.MainComponent + ".ts");
            Utils.CreateFolderForFile(path);
            File.WriteAllText(path, contents);
        }

        public override void GenerateBaseComponent(string viewPath)
        {
            PageTemplateConfig conf = _data.GetTemplate(viewPath);
            GenerateDataService(conf.Resource);

            using (var s = SW.Measure())
            {

                string path = Path.Combine(_paths.UIRoot, "Core\\" + _paths.CoreAppName, conf.Template + "Base.ts");
                output.Write("Writing Template " + conf.Template + "Base.ts...\t\t");

                if (!File.Exists(path))
                {
                    string[] serviced = new string[] { "Edit", "List", "Tree" };
                    bool isServiced = false;
                    BaseComponentTsModel model = new BaseComponentTsModel
                    {
                        Name = conf.Template.GetAfterLast("/") + "Base",
                        Resource = conf.Resource,

                    };

                    if (serviced.Contains(conf.BaseComponent))
                    {
                        isServiced = true;
                        model.ParentPath = "codeshell/baseComponents";
                        model.Parent = conf.BaseComponent + "ComponentBase";
                    }
                    else if (conf.BaseComponent == "Base")
                    {
                        model.ParentPath = "codeshell/baseComponents";
                        model.Parent = "BaseComponent";
                    }
                    else
                    {
                        model.ParentPath = _paths.CoreAppName+"/" + conf.BaseComponent;
                        model.Parent = conf.BaseComponent.GetAfterLast("/");
                    }

                    string template = _molds.GetBaseComponentMold(isServiced);
                    string component = _writer.FillStringParameters(template, model);
                    Utils.CreateFolderForFile(path);
                    File.WriteAllText(path, component);
                }

                WriteSuccess(s.Elapsed);
                output.WriteLine();
            }



        }

        public override void GenerateModuleDefinition(string moduleId, bool lazy)
        {
            NgModule mod = _data.GetNgModule(moduleId);
            using (var x = SW.Measure())
            {
                string moduleName = mod.Name + "Module";
                string modulePath = Path.Combine(_paths.UIRoot, mod.Name, "app", moduleName + ".ts");

                output.Write("Writing " + moduleName + ".ts...\t\t");

                mod.MainComponent = mod.MainComponent ?? "Base/Main/AppComponent";
                string moduleTemplate = _molds.AppModuleMold;
                var model = new ModuleTsModel
                {
                    Code = mod.Name,
                    BaseName = mod.BaseFolder,
                    MainComponentPath = "./" + mod.MainComponent,
                    MainComponentName = mod.MainComponent,
                    BaseAppModuleName = mod.BaseAppModulePath.GetAfterFirst("/"),
                    BaseAppModulePath = mod.BaseAppModulePath,

                };

                if (!lazy)
                {
                    foreach (var dom in mod.Domains)
                    {
                        model.ModuleImports += "import { " + dom.Name + "Module } from \"./" + dom.Name + "Module\";\n";
                        model.Modules += dom.Name + "Module,\n";
                    }
                }
                string module = _writer.FillStringParameters(moduleTemplate, model);

                Utils.CreateFolderForFile(modulePath);
                File.WriteAllText(modulePath, module);
                WriteSuccess(x.Elapsed, SuccessColumn);
            }
            output.WriteLine();

        }

        public override void GenerateComponent(string moduleName, PageRenderDTO dto)
        {
            PageConfig p = _data.GetPageConfig(moduleName, dto.ViewPath);
            using (var x = SW.Measure())
            {
                using (ColorSetter.Set(ConsoleColor.Cyan))
                    output.Write("\tTs: ");

                string path = Path.Combine(_paths.UIRoot, moduleName, "app", p.ComponentPath);
                string scriptPath = path + ".ts";

                string scriptTemplate = _molds.ComponentMold;

                Utils.CreateFolderForFile(scriptPath);

                string script = _writer.FillStringParameters(scriptTemplate, new ComponentTsModel
                {
                    BaseClassLocation = _paths.CoreAppName + "/" + p.ViewPath + "Base",
                    BaseClass = p.ViewPath.GetAfterLast("/") + "Base",
                    ComponentName = p.Name,
                    PageId = 0,
                    Domain = null,
                    Resource = p.ResourceName,
                    CollectionId = "null",
                    Selector = p.Name.GetAfterLast("/").LCFirst()
                });

                File.WriteAllText(scriptPath, script);


                WriteSuccess(x.Elapsed);

            }
        }

        public void PackVendor()
        {
            using (var x = SW.Measure())
            {
                ProcessStartInfo inf = new ProcessStartInfo
                {
                    WorkingDirectory = _paths.UIRoot,
                    FileName = "node",
                    Arguments = "node_modules/webpack/bin/webpack.js --config webpack.config.vendor.js"
                };
                output.WriteLine("Packing...");
                Process p = Process.Start(inf);
                p.WaitForExit();
                WriteSuccess(x.Elapsed);
            }
        }
        
        public override void GenerateDomainModule(string moduleCode, string domain, bool lazy = true)
        {
            DomainWithPages dom = _data.GetDomainModuleWithPages(moduleCode, domain);
            bool shared = dom.Name == "Shared";
            string temp = shared ? "SharedModule" : "DomainModule" + (lazy ? "_Lazy" : "");

            string templateContents = shared ? _molds.SharedModuleMold : _molds.GetDomainModuleMold(lazy);


            string filePath = Path.Combine(_paths.UIRoot, moduleCode, "app\\" + dom.Name + "Module" + ".ts");
            DomainTsModel model = new DomainTsModel
            {
                ComponentImports = "",
                Components = "",
                Name = dom.Name,
                Registrations = "",
                Routes = "",
                Lazy = lazy ? "" : "",
                BaseName = dom.BaseFolder
            };

            foreach (PageConfig p in dom.Pages.Where(d => !d.NoRoute || shared))
            {
                string component = p.Name;

                model.ComponentImports += p.GetImportString();
                model.Components += component + ",";
                model.Registrations += p.Registration;

                if (!shared)
                    model.Routes += "\t\t\t" + GetChildRoute(p) + ",\n";
            }

            string contents = _writer.FillStringParameters(templateContents, model);
            Utils.CreateFolderForFile(filePath);
            File.WriteAllText(filePath, contents);
        }

        public string GetChildRoute(PageConfig p)
        {
            string routeTemplate = _molds.RouteMold;
            string param = p.RouteParameters ?? "";

            string action = "\"anonymous\"";

            if (p.ResourcePrivilege != null || p.ResourceActionName != null)
                action = p.ResourceActionName == null ? "ResourceActions." + p.ResourcePrivilege : "\"" + p.ResourceActionName + "\"";

            string component = p.ComponentPath.GetAfterLast("/");

            RouteTsModel route = new RouteTsModel
            {
                Path = component + param,

                Component = component,
                Name = p.DomainName + "__" + component,
                Action = action,
                Navigate = p.AppearsInNavigation.ToString().ToLower(),
                Resource = p.ResourceName,
                Apps = p.Apps == null ? "null" : "[" + p.Apps + "]"
            };
            return _writer.FillStringParameters(routeTemplate, route);
        }



        public override void GenerateRoutes(string module, bool lazy)
        {
            NgModule mod = _data.GetNgModule(module);
            if (lazy)
                WriteRoutesLazy(mod);
            else
                WriteRoutes(mod);
        }

        internal string GetDomainNavigationList(DomainWithPages dom)
        {
            string children = "";
            foreach (var p in dom.Pages)
            {
                if (p.NoRoute || !p.AppearsInNavigation)
                    continue;
                string param = p.RouteParameters ?? "";

                if (string.IsNullOrEmpty(p.ResourceName) && p.ResourceActionName != "anonymous")
                {
                    throw new Exception("ResourceName is required");
                }

                string action = "\"anonymous\"";

                if (p.ResourcePrivilege != null || p.ResourceActionName != null)
                    action = p.ResourceActionName == null ? "ResourceActions." + p.ResourcePrivilege : "\"" + p.ResourceActionName + "\"";

                RouteTsModel route = new RouteTsModel
                {
                    Name = dom.Name + "__" + p.Name,
                    Action = action,
                    Navigate = p.AppearsInNavigation.ToString().ToLower(),
                    Resource = p.ResourceName,
                    Apps = p.Apps == null ? "null" : "[" + p.Apps + "]"
                };
                var s = "\n\t\t\t\t{{ name : \"{0}\", navigate: {1}, resource:\"{2}\", action: {3}, apps: {4} }},";
                children += string.Format(s,
                    route.Name,
                    route.Navigate,
                    route.Resource,
                    route.Action,
                    route.Apps);
            }
            return string.Format("{{ name : \"{0}\" ,children: [{1}]}}", dom.Name, children);
        }

        internal void WriteRoutesLazy(NgModule mod)
        {
            using (var x = SW.Measure())
            {
                string builderName = mod.Name + "Routes";
                string builderPath = Path.Combine(_paths.UIRoot, mod.Name, "app", mod.Name + "Routes.ts");

                output.Write("Writing " + builderName + ".ts...\t\t");

                var domains = mod.Domains;
                string routesTemplate = _molds.RoutesMold;
                var RoutesModel = new RoutesTsModel { BaseName = mod.BaseFolder, DefaultRoute = "" };

                if (mod.Default == null)
                    RoutesModel.DefaultRoute = "{ path: '', redirectTo: '/', pathMatch: 'full' }";
                else
                {
                    if (!mod.Domains.SelectMany(d => d.Pages).Any(d => d.ComponentPath == mod.Default && !d.NoRoute))
                    {
                        RoutesModel.ComponentImports += "import { " + mod.Default.GetAfterFirst("/") + " } from \"./" + mod.Default + "\"";
                    }

                    RoutesModel.DefaultRoute = $"{{ path: '', component: {mod.Default.GetAfterFirst("/")}, data:{{ action : 'anonymous' }}}}";
                }


                foreach (var domain in domains)
                {
                    string dom = domain.Name;
                    //RoutesModel.Routes += "{ path:\"" + dom + "\", loadChildren:\"./" + dom + "Module#" + dom + "Module?chunkName=" + Current.Code + "." + dom + "\" },\n\t";
                    RoutesModel.Routes += "{ path:\"" + dom + "\", loadChildren:\"./" + dom + "Module#" + dom + "Module\" },\n\t";
                    RoutesModel.DomainsData += "\t\t\t" + GetDomainNavigationList(domain) + ",\n";
                }

                AppendLocaleLoaders(RoutesModel);

                string builder = _writer.FillStringParameters(routesTemplate, RoutesModel);
                File.WriteAllText(builderPath, builder);

                WriteSuccess(x.Elapsed, SuccessColumn);
                output.WriteLine();
            }
        }

        internal void AppendDomain(RoutesTsModel RoutesModel, DomainWithPages dom)
        {
            string parentRouteTemplate = _molds.ParentRouteMold;
            ParentRouteTsModel parentRoute = new ParentRouteTsModel
            {
                DomainName = dom.Name,
                Children = ""
            };

            string sep = "";
            foreach (PageConfig p in dom.Pages)
            {
                if (!p.NoRoute)
                {
                    string component = p.ComponentPath.GetAfterLast("/");
                    RoutesModel.ComponentImports += p.GetImportString();
                    parentRoute.Children += sep + GetChildRoute(p) + ",\n";
                    sep = "\t\t";
                }
            }
            RoutesModel.Routes += _writer.FillStringParameters(parentRouteTemplate, parentRoute) + ",\n";
        }

        internal void WriteRoutes(NgModule mod)
        {
            using (var x = SW.Measure())
            {
                string builderName = mod.Name + ".routes";
                string builderPath = Path.Combine(_paths.UIRoot, mod.Name, "app", mod.Name + "Routes.ts");

                output.Write("Writing " + builderName + ".ts...\t\t");

                IEnumerable<DomainWithPages> domains = mod.Domains;
                string routesTemplate = _molds.RoutesMold;
                var RoutesModel = new RoutesTsModel { BaseName = mod.BaseFolder };
                if (mod.Default == null)
                    RoutesModel.DefaultRoute = "{ path: '', redirectTo: '/', pathMatch: 'full' }";
                else
                {
                    if (!mod.Domains.SelectMany(d => d.Pages).Any(d => d.ComponentPath == mod.Default && !d.NoRoute))
                    {
                        RoutesModel.ComponentImports += "import { " + mod.Default.GetAfterFirst("/") + " } from \"./" + mod.Default + "\"";
                    }
                    RoutesModel.DefaultRoute = $"{{ path: '', component: {mod.Default.GetAfterFirst("/")}, data:{{ action : 'anonymous' }}}}";
                }
                foreach (var dom in domains)
                {
                    AppendDomain(RoutesModel, dom);
                    RoutesModel.DomainsData += "\t\t\t" + GetDomainNavigationList(dom) + ",\n";
                }

                AppendLocaleLoaders(RoutesModel);

                string builder = _writer.FillStringParameters(routesTemplate, RoutesModel);
                File.WriteAllText(builderPath, builder);

                WriteSuccess(x.Elapsed, SuccessColumn);
                output.WriteLine();
            }
        }

        internal void AppendLocaleLoaders(RoutesTsModel model)
        {
            string[] locales = new string[] { "ar", "en" };
            foreach (string loc in locales)
            {
                model.LocalizationImports += "import { " + loc + "_Loader } from \"./Localization/" + loc + "/Loader\";\n";
                model.LocalizationLoaders += $"[\"{loc}\"]:new {loc}_Loader, ";
            }

        }
    }
}
