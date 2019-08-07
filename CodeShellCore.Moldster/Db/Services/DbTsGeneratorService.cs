using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CodeShellCore.Text;
using CodeShellCore.Helpers;
using CodeShellCore.Services;

using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Moldster.Models;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Moldster.Services.Internal;
using CodeShellCore.Moldster.Angular.Models;
using CodeShellCore.Cli;

namespace CodeShellCore.Moldster.Db.Services
{
    public class DbTsGeneratorService : ScriptGenerationService, IScriptGenerationService
    {

        static Dictionary<string, string> baseComponents = new Dictionary<string, string>
        {
            { "Edit","CodeShell/BaseComponents/EditComponentBase"},
            { "List","CodeShell/BaseComponents/ListComponentBase"},
            { "Tree","CodeShell/BaseComponents/TreeComponentBase"},
            { "Select","CodeShell/BaseComponents/SelectComponentBase"},
        };

        IConfigUnit _unit;
        PageControlsService _controls;
        public DbTsGeneratorService(
            WriterService se,
            PathProvider paths,
            IMoldProvider mold,
            IConfigUnit unit,
            PageControlsService controls,
            IMappedEnumerations enums) : base(se, mold, paths, enums)
        {
            _unit = unit;
            _controls = controls;
        }

        public override void GenerateDomainModule(string tenantCode, string domain, bool lazy = true)
        {
            Console.Write($"Generating {domain}Module : ");
            GotoColumn(resultcol);
            bool shared = domain == "Shared";
            DomainWithPagesDTO dom = null;

            if (shared)
                dom = new DomainWithPagesDTO
                {
                    DomainName = "Shared",
                    Pages = _unit.PageRepository.GetSharedPagesForRouting(tenantCode)
                };
            else
                dom = new DomainWithPagesDTO
                {
                    DomainName = domain,
                    Pages = _unit.PageRepository.GetDomainPagesForRouting(tenantCode, domain)
                };

            string template = shared ? _molds.SharedModuleMold : _molds.GetDomainModuleMold(lazy);
            string filePath = Path.Combine(_paths.UIRoot, tenantCode, "app\\" + dom.DomainName + "Module.ts");

            DomainTsModel model = new DomainTsModel
            {
                ComponentImports = "",
                Components = "",
                Name = dom.DomainName,
                Registrations = "",
                Routes = "",
                Lazy = lazy ? "" : "",
                BaseName = _paths.CoreAppName
            };

            foreach (PageDTO p in dom.Pages.Where(d => d.Page.HasRoute || shared))
            {
                string component = p.ComponentName;

                model.ComponentImports += p.GetImportString();
                model.Components += component + ",";
                model.Registrations += p.Registration;

                if (!shared)
                    model.Routes += "\t\t\t" + ChildRoute(p) + ",\n";
            }

            string contents = _writer.FillStringParameters(template, model);
            Utils.CreateFolderForFile(filePath);
            File.WriteAllText(filePath, contents);
            WriteSuccess();
            Console.WriteLine();
        }

        public override void GenerateComponent(string module, string domain, string viewPath)
        {
            using (ColorSetter.Set(ConsoleColor.DarkRed))
                Console.Write(" Ts: ");
            PageDTO p = _unit.PageRepository.FindSingleAs(PageDTO.ExpressionForRendering, d => d.TenantDomain.Tenant.Code == module && d.TenantDomain.Domain.Name == domain && d.ViewPath.Contains(viewPath));

            string scriptTemplate = "";
            if (p.ParentHasResource)
                scriptTemplate = _molds.ComponentMold;
            else if (p.ResourceName != null)
                scriptTemplate = _molds.LookupComponent;
            else
                scriptTemplate = _molds.BasicComponent;

            if (p.BaseScript == null)
            {
                WriteException(new Exception("Please process template first!!"), false);
                WriteFailed();
                return;
            }

            string script = _writer.FillStringParameters(scriptTemplate, new ComponentTsModel
            {
                BaseClassLocation = p.BaseScript,
                BaseClass = p.BaseScript.GetAfterLast("/"),
                ComponentName = p.Page.Name,
                PageId = p.Page.Id,
                Domain = p.DomainName,
                Resource = p.ResourceName,
                Selector = p.Page.Name.LCFirst(),

                CollectionId = p.CollectionId == null ? "null" : "'" + p.CollectionId + "'"
            });

            string scriptPath = Path.Combine(_paths.UIRoot, module, "app", viewPath + ".ts");

            Utils.CreateFolderForFile(scriptPath);
            File.WriteAllText(scriptPath, script);

            WriteSuccess();
        }

        public override void GenerateBaseComponent(string viewPath)
        {
            PageCategoryDTO p = _unit.PageCategoryRepository.FindSingleAs(PageCategoryDTO.Expression, d => d.ViewPath == viewPath);
            if (p == null)
                throw new ArgumentOutOfRangeException($"PageCategory '{viewPath}' doesn't exist");

            string name = p.Category.ScriptPath.GetAfterLast("/");
            bool serviced = false;
            BaseComponentTsModel mod = new BaseComponentTsModel
            {
                Name = name,
                Domain = p.Domain ?? "",
                Resource = p.Resource ?? ""
            };

            if (p.Category.BaseComponent != null)
            {
                serviced = p.Resource != null;

                if (!baseComponents.TryGetValue(p.Category.BaseComponent, out string parent))
                {
                    parent = p.Category.BaseComponent;
                }
                else if (p.Resource == null)
                {
                    mod.Resource = "DefaultHttp";
                    mod.ServicePath = "CodeShell/Http";
                }

                if (p.Resource != null)
                    mod.ServicePath = $"{_paths.CoreAppName}/{p.Domain}/Http";

                mod.ParentPath = parent;
            }
            else
            {
                mod.ParentPath = "CodeShell/BaseComponents/BaseComponent";
            }

            mod.Parent = mod.ParentPath.GetAfterLast("/");

            string baseComponentTemplatePath = _molds.GetBaseComponentMold(serviced);

            string contents = _writer.FillStringParameters(baseComponentTemplatePath, mod);
            string path = Path.Combine(_paths.UIRoot, "Core\\" + _paths.CoreAppName, viewPath + "Base.ts");

            File.WriteAllText(path, contents);

        }

        public override void GenerateMainComponent(string mod)
        {
            using (ColorSetter.Set(ConsoleColor.DarkRed))
                Console.Write(" Ts: ");

            string mainCompBase = _unit.TenantRepository.GetSingleValue(d => d.MainComponentBase, d => d.Code == mod);
            string temp = _molds.MainComponentMold;
            var model = new AppComponentModel
            {
                Name = "AppComponent",
                BaseComponentName = mainCompBase.GetAfterLast("/") + "Base",
                BaseComponentPath = _paths.CoreAppName + "/" + mainCompBase + "Base"
            };
            string contents = _writer.FillStringParameters(temp, model);
            string path = Path.Combine(_paths.UIRoot, mod, "app", "AppComponent.ts");
            Utils.CreateFolderForFile(path);
            File.WriteAllText(path, contents);

            WriteSuccess();

        }

        public override void GenerateModuleDefinition(string modCode, bool lazy)
        {
            string moduleName = modCode + "Module";
            string modulePath = Path.Combine(_paths.UIRoot, modCode, "app", moduleName + ".ts");

            Console.Write("Generating " + moduleName + ".ts : ");
            var main = _unit.TenantRepository.GetSingleValue(d => d.MainComponentBase, d => d.Code == modCode);
            var tempModel = new ModuleTsModel
            {
                Code = modCode,
                Modules = "",
                ModuleImports = "",
                MainComponentName = main,
                MainComponentPath = "./" + main,
                BaseName = _paths.CoreAppName,
                BaseAppModuleName = _paths.CoreAppName + "BaseModule",
                BaseAppModulePath = _paths.CoreAppName + "/" + _paths.CoreAppName + "BaseModule"
            };

            if (!lazy)
            {
                List<string> modules = _unit.TenantDomainRepository.FindAs(d => d.Domain.Name, d => d.Tenant.Code == modCode);
                foreach (var mod in modules)
                {
                    tempModel.ModuleImports += $"import {{ {mod}Module }} from \"./{mod}Module\";\r";
                    tempModel.Modules += $"{mod}Module, ";
                }
            }

            string moduleTemplate = _molds.AppModuleMold;
            string contents = _writer.FillStringParameters(moduleTemplate, tempModel);
            File.WriteAllText(modulePath, contents);

            GotoColumn(resultcol);
            WriteSuccess();
            Console.WriteLine();
        }

        public override void GenerateRoutes(string mod, bool lazy)
        {
            Console.Write($"Generating Routes [{mod}Routes.ts] : ");
            if (lazy)
                GenerateRoutesLazy(mod);
            else
                GenerateRoutes(mod);
            GotoColumn(resultcol);
            WriteSuccess();
            Console.WriteLine();
        }


        #region private methods
        private void GenerateRoutes(string modCode)
        {
            long modId = _unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);
            string fileName = modCode + "Routes";
            string filePath = Path.Combine(_paths.UIRoot, modCode, "app", fileName + ".ts");

            IEnumerable<DomainWithPagesDTO> domains = _controls.GetDomainWithPages(modId);
            string routesTemplate = _molds.RoutesMold;

            var tempModel = new RoutesTsModel
            {
                LocalizationImports = "",
                ComponentImports = "",
                LocalizationLoaders = "",
                Routes = "",
                Registration = "",
                BaseName = _paths.CoreAppName
            };

            foreach (var dom in domains)
            {
                AppendDomain(dom, tempModel);
                tempModel.DomainsData += "\t\t\t" + DomainRouteData(dom) + ",\n";
            }

            AppendLocaleLoaders(tempModel);

            string contents = _writer.FillStringParameters(routesTemplate, tempModel);
            File.WriteAllText(filePath, contents);
        }

        private void AppendGuide(RoutesTsModel mod)
        {
            var p = new PageDTO
            {
                ActionName = "anonymous",
                DomainName = "Guide",
                PageIdentifier = "Guide__Guide",

                Page = new Page
                {
                    AppearsInNavigation = false,
                    Apps = null,
                    CanEmbed = false,
                    HasRoute = true,
                    ViewPath = "Guide/Guide",

                }
            };

            DomainWithPagesDTO guide = new DomainWithPagesDTO
            {
                DomainName = "Guide",
                Pages = new List<PageDTO> { p }
            };
            AppendDomain(guide, mod);
        }

        private void AppendDomain(DomainWithPagesDTO dom, RoutesTsModel mod)
        {
            string parentRouteTemplate = _molds.ParentRouteMold;
            ParentRouteTsModel parentRoute = new ParentRouteTsModel
            {
                DomainName = dom.DomainName,
                Children = ""
            };

            string sep = "";
            foreach (PageDTO p in dom.Pages)
            {
                if (p.Page.HasRoute)
                {
                    string component = p.Page.ViewPath.GetAfterLast("/");
                    mod.ComponentImports += "import { " + component + " } from \"./" + p.Page.ViewPath + "\";\n";
                    parentRoute.Children += sep + ChildRoute(p) + ",\n";
                    sep = "\t\t";
                }
            }
            mod.Routes += _writer.FillParameters(parentRouteTemplate, parentRoute) + ",\n";
        }

        private void GenerateRoutesLazy(string modCode)
        {
            long modId = _unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);
            string fileName = modCode + "Routes";
            string filePath = Path.Combine(_paths.UIRoot, modCode, "app", fileName + ".ts");

            var domains = _controls.GetDomainWithPages(modId);
            string routesTemplate = _molds.RoutesMold;

            var tempModel = new RoutesTsModel
            {
                LocalizationImports = "",
                ComponentImports = "",
                LocalizationLoaders = "",
                Routes = "",
                Registration = "",
                BaseName = _paths.CoreAppName
            };

            foreach (var domain in domains)
            {
                string dom = domain.DomainName;
                //RoutesModel.Routes += "{ path:\"" + dom + "\", loadChildren:\"./" + dom + "Module#" + dom + "Module?chunkName=" + Current.Code + "." + dom + "\" },\n\t";
                tempModel.Routes += "{ path:\"" + dom + "\", loadChildren:\"./" + dom + "Module#" + dom + "Module\" },\n\t";
                tempModel.DomainsData += "\t\t\t" + DomainRouteData(domain) + ",\n";
            }

            AppendLocaleLoaders(tempModel);

            string builder = _writer.FillStringParameters(routesTemplate, tempModel);
            File.WriteAllText(filePath, builder);
        }

        private void AppendLocaleLoaders(RoutesTsModel mod)
        {
            string[] locales = new string[] { "ar", "en" };
            foreach (string loc in locales)
            {
                mod.LocalizationImports += "import { " + loc + "_Loader } from \"./Localization/" + loc + "/Loader\";\n";
                mod.LocalizationLoaders += $"[\"{loc}\"]:new {loc}_Loader, ";
            }
        }

        private string DomainRouteData(DomainWithPagesDTO dom)
        {
            string children = "";
            foreach (var p in dom.Pages)
            {
                if (!p.Page.HasRoute || !p.Page.AppearsInNavigation)
                    continue;
                string param = p.Page.RouteParameters ?? "";
                string action = p.ActionName == null ? "ResourceActions." + p.Page.PrivilegeType : "\"" + p.ActionName + "\"";

                RouteTsModel route = new RouteTsModel
                {
                    Name = p.PageIdentifier,
                    Action = action,
                    Navigate = p.Page.AppearsInNavigation.ToString().ToLower(),
                    Resource = p.ResourceName,
                    Apps = p.Page.Apps == null ? "null" : "[" + p.Page.Apps + "]"
                };
                var s = "\n\t\t\t\t{{ name : \"{0}\", navigate: {1}, resource:\"{2}\", action: {3}, apps: {4} }},";
                children += string.Format(s,
                    route.Name,
                    route.Navigate,
                    route.Resource,
                    route.Action,
                    route.Apps);
            }
            return string.Format("{{ name : \"{0}\" ,children: [{1}]}}", dom.DomainName, children);
        }

        private string ChildRoute(PageDTO p)
        {
            string routeTemplate = _molds.RouteMold;
            string param = p.Page.RouteParameters ?? "";
            string action = p.ActionName == null ? "ResourceActions." + p.Page.PrivilegeType : "\"" + p.ActionName + "\"";
            string component = p.Page.ViewPath.GetAfterLast("/");

            RouteTsModel route = new RouteTsModel
            {
                Path = component + param,

                Component = component,
                Name = p.PageIdentifier,
                Action = action,
                Navigate = p.Page.AppearsInNavigation.ToString().ToLower(),
                Resource = p.ResourceName,
                Apps = p.Page.Apps == null ? "null" : "[" + p.Page.Apps + "]"
            };
            return _writer.FillStringParameters(routeTemplate, route);
        }

        #endregion
    }

}
