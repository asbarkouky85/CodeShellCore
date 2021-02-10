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
using CodeShellCore.Files;
using System.Text.RegularExpressions;
using CodeShellCore.Moldster.Angular;
using CodeShellCore.CLI;
using CodeShellCore.Moldster.Db;
using CodeShellCore.Moldster.Definitions;

namespace CodeShellCore.Moldster.Services.Db
{
    public class DbTsGeneratorService : ScriptGenerationService, IScriptGenerationService
    {

        static string[] baseComponents = new[] { "Edit", "List", "Tree", "Select" };

        IConfigUnit _unit;
        private readonly IFileHandler fileHandler;
        private readonly IOutputWriter output;
        IPageControlDataService _controls;
        public DbTsGeneratorService(
            WriterService se,
            IPathsService paths,
            IMoldProvider mold,
            IConfigUnit unit,
            IFileHandler fileHandler,
            IPageControlDataService controls,

            IOutputWriter output) : base(se, mold, paths, output)
        {
            _unit = unit;
            this.fileHandler = fileHandler;
            _controls = controls;
            this.output = output;
        }

        public override void GenerateDomainModule(string moduleCode, string domId, bool lazy = true)
        {
            long? id = null;
            if (domId == "Shared")
                id = -1;
            else if (domId != null)
                id = _unit.DomainRepository.GetDomainByPath(domId).Id;
            GenerateDomainModuleById(moduleCode, id, lazy);
        }

        public override void GenerateDomainModuleById(string moduleCode, long? domId, bool lazy = true)
        {
            if (domId == -1)
            {
                GenerateDomainRecursive(new DomainWithPagesDTO { DomainName = "Shared" }, moduleCode, null, lazy);
                return;
            }

            var doms = new List<DomainWithPagesDTO>();

            doms = _unit.DomainRepository.GetByTenantCodeForRouting(moduleCode, domId);
            var newList = new List<DomainWithPagesDTO>();
            if (domId == null)
                newList = doms.Where(d => d.ParentId == null).ToList();
            else
                newList = doms.Where(d => d.Id == domId).ToList();

            foreach (var item in newList)
                item.AppendChildren(doms);

            string parent = domId == null ? null : _unit.DomainRepository.GetValue(domId.Value, d => d.NameChain);
            if (parent != null)
            {
                parent = new Regex("^/").Replace(parent, "");
                parent = new Regex("/$").Replace(parent, "");
                if (parent.IndexOf('/') > -1)
                    parent = parent.GetBeforeLast("/");
                else
                    parent = null;
            }

            foreach (var item in newList)
                GenerateDomainRecursive(item, moduleCode, parent, lazy);
        }

        void GenerateDomainRecursive(DomainWithPagesDTO dom, string tenantCode, string parentDomain = null, bool lazy = true)
        {
            output.Write($"Generating {dom.DomainName}Module : ");
            GotoColumn(SuccessCol);
            bool shared = dom.DomainName == "Shared";
            string template = shared ? _molds.SharedModuleMold : _molds.GetDomainModuleMold(lazy);
            string filePath = Path.Combine(_paths.UIRoot, tenantCode, "app\\" + (parentDomain != null ? "\\" + parentDomain + "\\" : "") + dom.DomainName + "\\" + dom.DomainName + "Module.ts");

            if (shared)
                dom.Pages = _unit.PageRepository.GetSharedPagesForRouting(tenantCode);
            else
                dom.Pages = _unit.PageRepository.GetDomainPagesForRouting(tenantCode, dom.Id);

            int c = 0;
            if (parentDomain != null)
            {
                c = parentDomain.Count(d => d == '\\') + 1;
            }

            string rootPath = "../";
            for (var i = 0; i < c; i++)
                rootPath += "../";

            DomainTsModel model = new DomainTsModel
            {
                ComponentImports = "",
                Components = "",
                Name = dom.DomainName,
                Registrations = "",
                Routes = "",
                Lazy = lazy ? "" : "",
                BaseName = _paths.CoreAppName,
                BaseAppModuleName = _paths.CoreAppName + "BaseModule",
                BaseAppModulePath = _paths.CoreAppName + "/" + _paths.CoreAppName + "BaseModule",
                PathToRoot = rootPath
            };

            foreach (PageDTO p in dom.Pages.Where(d => d.Page.HasRoute || shared))
            {
                string component = p.ComponentName;

                model.ComponentImports += p.GetImportString(!shared);
                if (!p.Page.CanEmbed || shared)
                    model.Components += component + ",";
                model.Registrations += p.Registration;

                if (!shared)
                    model.Routes += "\t\t\t" + ChildRoute(p) + ",\n";
            }

            if (dom.SubDomains != null)
            {
                foreach (DomainWithPagesDTO dp in dom.SubDomains)
                {
                    model.Routes += dp.LazyLoadingRoute;
                }
            }


            string contents = _writer.FillStringParameters(template, model);
            Utils.CreateFolderForFile(filePath);
            File.WriteAllText(filePath, contents);
            WriteSuccess();
            output.WriteLine();

            if (dom.SubDomains != null && dom.SubDomains.Any())
            {
                foreach (var d in dom.SubDomains)
                    GenerateDomainRecursive(d, tenantCode, (parentDomain == null ? "" : parentDomain + "\\") + dom.DomainName, lazy);
            }
        }

        public override void GenerateComponent(string module, PageRenderDTO viewPath)
        {
            using (ColorSetter.Set(ConsoleColor.DarkRed))
                output.Write(" Ts: ");
            PageDTO p = _unit.PageRepository.FindSingleAs(PageDTO.ExpressionForRendering, d => d.Id == viewPath.Id);

            string scriptTemplate = "";
            if (p.ParentHasResource)
                scriptTemplate = _molds.ComponentMold;
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

            string scriptPath = Path.Combine(_paths.UIRoot, p.TenantCode, "app", p.Page.ViewPath + ".ts");

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

            if (!string.IsNullOrEmpty(p.Category.BaseComponent))
            {
                serviced = p.Resource != null;


                if (baseComponents.Contains(p.Category.BaseComponent))
                {
                    mod.Parent = p.Category.BaseComponent + "ComponentBase";
                    mod.ParentPath = "codeshell/baseComponents";
                    if (p.Resource == null)
                    {
                        mod.Resource = "DefaultHttp";
                        mod.ServicePath = "codeshell/http";
                    }
                }
                else if (p.Category.BaseComponent != null)
                {
                    mod.ParentPath = p.Category.BaseComponent;
                    mod.Parent = mod.ParentPath.GetAfterLast("/");
                }


                if (p.Resource != null)
                {
                    string folder = "Http";

                    if (p.ResourceDomain != null)
                    {
                        folder = Utils.CombineUrl(new Regex("^/").Replace(p.ResourceDomain, ""), folder);
                    }

                    mod.ServicePath = Utils.CombineUrl($"{_paths.CoreAppName}/{folder}");
                }
            }
            else
            {
                mod.Parent = "BaseComponent";
                mod.ParentPath = "codeshell/baseComponents";
            }

            string baseComponentTemplatePath = _molds.GetBaseComponentMold(serviced);

            string contents = _writer.FillStringParameters(baseComponentTemplatePath, mod);
            string path = Path.Combine(_paths.UIRoot, "Core\\" + _paths.CoreAppName, viewPath + "Base.ts");

            File.WriteAllText(path, contents);

        }

        public override void GenerateMainComponent(string mod)
        {
            using (ColorSetter.Set(ConsoleColor.DarkRed))
                output.Write(" Ts: ");

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

            output.Write("Generating " + moduleName + ".ts : ");
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
                List<string> modules = _unit.DomainRepository.FindAs(d => d.Name, d => d.Pages.Any(p => p.Tenant.Code == modCode) && d.ParentId == null);
                foreach (var mod in modules)
                {
                    tempModel.ModuleImports += $"import {{ {mod}Module }} from \"./{mod}Module\";\r";
                    tempModel.Modules += $"{mod}Module, ";
                }
            }

            var homePage = _unit.PageRepository.GetHomePagePath(modCode);
            if (homePage != null)
            {
                tempModel.ModuleImports += AngularUtils.ComponentImport(homePage, out string pageName);
                tempModel.Declarations += pageName;
            }

            string moduleTemplate = _molds.AppModuleMold;
            string contents = _writer.FillStringParameters(moduleTemplate, tempModel);
            File.WriteAllText(modulePath, contents);

            GotoColumn(SuccessCol);
            WriteSuccess();
            output.WriteLine();
        }

        public override void GenerateRoutes(string mod, bool lazy)
        {
            output.Write($"Generating Routes [{mod}Routes.ts] : ");
            if (lazy)
                GenerateRoutesLazy(mod);
            else
                GenerateRoutes(mod);
            GotoColumn(SuccessCol);
            WriteSuccess();
            output.WriteLine();
        }

        public override void GeneratePageCategory(long id)
        {
            string serviceName = null;
            string baseComponent = null;
            bool serviceCreated = false;
            var p = _unit.PageCategoryRepository.FindSingleAs(d => new CategoryBaseComponentDTO
            {
                ScriptPath = d.ScriptPath,
                ViewPath = d.ViewPath,
                Name = d.Name,
                Resource = d.ResourceId != null ? d.Resource.Name : null,
                ResourceDomain = d.ResourceId != null ? d.Resource.Domain.Name : null
            }, d => d.Id == id);

            if (p.ScriptPath == null)
            {
                string folder = p.ViewPath.GetBeforeLast("/");
                var cat = _unit.PageCategoryRepository.FindSingle(id);
                cat.ScriptPath = _paths.CoreAppName + "/" + folder + "/" + p.Name + "Base";
                p.ScriptPath = cat.ScriptPath;
                _unit.SaveChanges();
            }

            if (p.Resource != null)
            {
                serviceName = p.Resource + "Service.ts";
                serviceCreated = GenerateDataService(p.Resource, p.ResourceDomain);
            }

            string baseComponentPath = Path.Combine(_paths.UIRoot, "Core", p.ScriptPath + ".ts");
            Utils.CreateFolderForFile(baseComponentPath);

            if (!File.Exists(baseComponentPath))
            {
                GenerateBaseComponent(p.ViewPath);
                baseComponent = p.Name + "Base";
            }

            if (serviceCreated || baseComponent != null)
            {
                output.WriteLine();
                output.Write("New files generated : ");
                GotoColumn(6);
                output.Write("[ ");
                if (serviceCreated)
                {
                    using (output.Set(ConsoleColor.Yellow))
                        output.Write(serviceName);
                }

                if (baseComponent != null)
                {
                    if (serviceCreated)
                        output.Write(" , ");
                    using (output.Set(ConsoleColor.Cyan))
                        output.Write(baseComponent + ".ts");
                }
                output.Write(" ]");
            }

            output.WriteLine();
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

            IEnumerable<DomainWithPagesDTO> domains = _unit.DomainRepository.GetParentModules(modId);
            IEnumerable<NavigationGroupDTO> navs = _unit.NavigationGroupRepository.GetTenantNavs(modId);
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

            string home = _unit.PageRepository.GetHomePagePath(modCode);
            if (home != null)
            {
                tempModel.ComponentImports += AngularUtils.ComponentImport(home, out string name);
                tempModel.Routes += HomeRoute(name);
            }

            foreach (var domain in domains)
            {
                string dom = domain.DomainName;
                tempModel.Routes += domain.LazyLoadingRoute;
            }
            string sep = "";
            foreach (var nav in navs)
            {
                tempModel.DomainsData += sep + GetNavigationObject(nav);
                sep = ",\n\t\t\t";
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

        private string GetNavigationObject(NavigationGroupDTO dto)
        {
            string children = "";
            foreach (var p in dto.Pages)
            {
                string param = p.RouteParameters ?? "";
                string action = p.ActionName == null ? "ResourceActions." + p.PrivilegeType : "\"" + p.ActionName + "\"";
                RouteTsModel route = new RouteTsModel
                {
                    Name = p.PageIdentifier,
                    Action = action,
                    Navigate = "true",
                    Resource = p.ResourceName,
                    Apps = p.Apps == null ? "null" : "[" + p.Apps + "]",
                    Url = p.Url
                };
                var s = "\n\t\t\t\t\t{{ name: \"{0}\", navigate: {1}, resource:\"{2}\", action: {3}, apps: {4} , url: \"{5}\"}},";
                children += string.Format(s,
                    route.Name,
                    route.Navigate,
                    route.Resource,
                    route.Action,
                    route.Apps,
                    route.Url);
            }
            return string.Format("{{\n\t\t\t\tname: \"{0}\" ,\n\t\t\t\tchildren: [{1}]\n\t\t\t}}", dto.Name, children);
        }

        private string HomeRoute(string name)
        {
            return $"{{ path: '', component: {name}, data: {{ action: 'anonymous' }} }},\n";
        }

        private string ChildRoute(PageDTO p)
        {
            string routeTemplate = _molds.RouteMold;
            string param = p.Page.RouteParameters ?? "";
            string action = p.ActionName == null ? "ResourceActions." + p.Page.PrivilegeType ?? "view" : "\"" + p.ActionName + "\"";
            string component = p.Page.ViewPath.GetAfterLast("/");

            RouteTsModel route = new RouteTsModel
            {
                Path = component + param,

                Component = component,
                Name = p.PageIdentifier,
                Action = action,
                Navigate = "false",
                Resource = p.ResourceName,
                Apps = p.Page.Apps == null ? "null" : "[" + p.Page.Apps + "]"
            };
            return _writer.FillStringParameters(routeTemplate, route);
        }

        #endregion
    }

}
