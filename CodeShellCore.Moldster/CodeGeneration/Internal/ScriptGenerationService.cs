using CodeShellCore.Cli;
using CodeShellCore.Files;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Angular.Models;
using CodeShellCore.Moldster.Angular;
using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Models;
using CodeShellCore.Services;
using CodeShellCore.Text.ResourceReader;
using CodeShellCore.Text;
using CodeShellCore.Types;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System;
using CodeShellCore.Moldster.Razor;

namespace CodeShellCore.Moldster.CodeGeneration.Internal
{
    public class ScriptGenerationService : ConsoleService, IScriptGenerationService
    {
        InstanceStore<object> _store;
        static string[] baseComponents = new[] { "Edit", "List", "Tree", "Select" };

        private MoldsterModuleOptions _opts;
        protected WriterService _writer;

        protected IMoldProvider _molds => _store.GetInstance<IMoldProvider>();
        private IUIFileNameService _fileNameService => _store.GetInstance<IUIFileNameService>();
        protected IPathsService _paths => _store.GetInstance<IPathsService>();
        protected IConfigUnit _unit => _store.GetInstance<IConfigUnit>();
        protected IFileHandler _fileHandler => _store.GetInstance<IFileHandler>();
        protected ILocalizationService _localization => _store.GetInstance<ILocalizationService>();
        protected IPageControlDataService _controls => _store.GetInstance<IPageControlDataService>();

        public ScriptGenerationService(
            IServiceProvider prov,
            IOptions<MoldsterModuleOptions> opt,
            IOutputWriter output) : base(output)
        {
            _store = new InstanceStore<object>(prov);
            _opts = opt.Value;
            _writer = new WriterService();
        }

        public void GenerateMainFile(string moduleCode, bool addStyle = false)
        {
            string bootPath = _fileNameService.GetSrcFolderPath("main-" + moduleCode.LCFirst(), ".ts", keepNameformat: true);
            string pollyPath = _fileNameService.GetSrcFolderPath("polyfills");
            string indexPath = _fileNameService.GetSrcFolderPath("index", ".html");
            string dec = _fileNameService.GetSrcFolderPath("declarations.d");

            if (!File.Exists(bootPath))
            {
                Out.Write("Generating main.ts...  \t\t\t");
                string bootTemplate = _molds.BootMold;
                string boot = _writer.FillStringParameters(bootTemplate, new BootTsModel
                {
                    Code = moduleCode.UCFirst(),
                    ModulePath = _fileNameService.ApplyConvension(moduleCode + "/app", AppParts.Module)
                });
                File.WriteAllText(bootPath, boot);
                WriteSuccess();
                Out.WriteLine();
            }

            if (!File.Exists(pollyPath))
            {
                Out.Write("Generating polyfills.ts...  \t\t\t");
                string pollyTemplate = Properties.Resources.pollyfills_ts;
                File.WriteAllText(pollyPath, pollyTemplate);
                WriteSuccess();
                Out.WriteLine();
            }

            if (!File.Exists(indexPath))
            {
                Out.Write("Generating index.html...  \t\t\t");
                string pollyTemplate = Properties.Resources.index_html;
                File.WriteAllText(indexPath, pollyTemplate);
                WriteSuccess();
                Out.WriteLine();
            }

            if (!File.Exists(dec))
            {
                Out.Write("Generating declarations.d.ts...  \t\t\t");
                string pollyTemplate = Properties.Resources.declarations_d;
                File.WriteAllText(dec, pollyTemplate);
                WriteSuccess();
                Out.WriteLine();
            }
        }

        public bool GenerateHttpService(string resource, string domain = null)
        {
            string folder = _fileNameService.GetHttpServiceFolder();

            if (domain != null)
            {
                domain = new Regex("^/").Replace(domain, "").Replace("/", "\\");
                folder = _fileNameService.GetHttpServiceFolder(domain);
            }

            string servicePath = Path.Combine(_paths.UIRoot, folder + "/" + _fileNameService.ApplyConvension(resource, AppParts.Service) + ".ts");
            Utils.CreateFolderForFile(servicePath);
            if (!File.Exists(servicePath))
            {
                string serviceTemplate = _molds.ServiceMold;
                string service = _writer.FillStringParameters(serviceTemplate, new ServiceTsModel { Resource = resource });
                File.WriteAllText(servicePath, service);

                string httpPath = Path.Combine(_paths.UIRoot, folder, "index.ts"); ;
                List<string> lst = new List<string>();
                if (File.Exists(httpPath))
                {
                    string[] lines = File.ReadAllLines(httpPath);
                    lst.AddRange(lines);
                }
                lst.Add("export * from \"./" + _fileNameService.ApplyConvension(resource, AppParts.Service) + "\";");
                File.WriteAllLines(httpPath, lst);
                return true;
            }
            return false;

        }

        public virtual void GenerateModuleDefinitionByPage(PageRenderDTO dto)
        {
            var data = _unit.PageRepository.FindSingleAs(
                d => new { d.Domain.Chain, HasNav = d.NavigationPages.Any(), d.Tenant.Code },
                d => d.Id == dto.Id
                );

            var dom = _unit.DomainRepository.GetSingleValue(d => d.Id, d => data.Chain.Contains("|" + d.Id + "|") && d.ParentId == null);
            GenerateDomainModuleById(data.Code, dom);
            if (data.HasNav)
            {
                GenerateRoutes(data.Code);
            }
        }

        public virtual void GenerateComponent(string module, PageRenderDTO viewPath, PageJsonData data)
        {
            PageDTO p = _unit.PageRepository.FindSingleForRendering(d => d.Id == viewPath.Id);
            string scriptPath = _fileNameService.GetComponentFilePath(p.TenantCode, p.Page.ViewPath) + ".ts";

            using (Out.Set(ConsoleColor.DarkRed))
            {
                Out.Write(" Ts: ");
            }


            if (File.Exists(scriptPath) && !_opts.ReplaceComponentScripts)
            {
                WriteColored("Exists", ConsoleColor.Cyan);
                Out.WriteLine();
                return;
            }


            string scriptTemplate = "";
            if (p.ParentHasResource)
                scriptTemplate = _molds.ComponentMold;
            else
                scriptTemplate = _molds.BasicComponent;

            if (p.BaseViewPath == null)
            {
                WriteException(new Exception("Please process template first!!"), false);
                WriteFailed();
                return;
            }

            string script = _writer.FillStringParameters(scriptTemplate, new ComponentTsModel
            {
                BaseClassLocation = _fileNameService.GetBaseComponentFilePath(p.BaseViewPath, true),
                BaseClass = p.BaseViewPath.GetAfterLast("/") + "Base",
                ComponentName = p.Page.Name,
                TemplateName = _fileNameService.ApplyConvension(p.Page.Name, AppParts.Component),

                Domain = p.DomainName,
                Resource = p.ResourceName,
                Selector = _fileNameService.GetComponentSelector(p.Page.Name),
                ViewParams = data.ViewParams.ToJson(new Newtonsoft.Json.JsonSerializerSettings { StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.EscapeHtml }),
                Sources = data.Sources.ToJsonIndent(),
                CollectionId = p.CollectionId == null ? "null" : "'" + p.CollectionId + "'"
            });

            Utils.CreateFolderForFile(scriptPath);
            File.WriteAllText(scriptPath, script);

            WriteSuccess();
        }

        public virtual void GenerateBaseComponent(string viewPath)
        {
            PageCategoryDTO p = _unit.PageCategoryRepository.FindSingleAs(PageCategoryDTO.Expression, d => d.ViewPath == viewPath);
            if (p == null)
                throw new ArgumentOutOfRangeException($"PageCategory '{viewPath}' doesn't exist");

            string name = p.Category.ViewPath.GetAfterLast("/") + "Base";
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
                    mod.ParentPath = "codeshell/base-components";
                    if (p.Resource == null)
                    {
                        mod.Resource = "DefaultHttp";
                        mod.ServicePath = "codeshell/http";
                    }
                }
                else if (p.Category.BaseComponent != null)
                {
                    mod.ParentPath = _fileNameService.ApplyConvension(p.Category.BaseComponent, AppParts.BaseComponent);
                    mod.Parent = mod.ParentPath.GetAfterLast("/");
                }


                if (p.Resource != null)
                {
                    string folder = _fileNameService.GetHttpServiceFolder(null, true);

                    if (p.ResourceDomain != null)
                    {
                        folder = _fileNameService.GetHttpServiceFolder(new Regex("^/").Replace(p.ResourceDomain, ""), true);
                    }

                    mod.ServicePath = Utils.CombineUrl(folder, _fileNameService.ApplyConvension(p.Resource, AppParts.Service));
                }
            }
            else
            {
                mod.Parent = "BaseComponent";
                mod.ParentPath = "codeshell/base-components";
            }

            string baseComponentTemplatePath = _molds.GetBaseComponentMold(serviced);

            string contents = _writer.FillStringParameters(baseComponentTemplatePath, mod);
            string path = _fileNameService.GetBaseComponentFilePath(viewPath) + ".ts"; // Path.Combine(_paths.UIRoot, "Core\\" + _paths.CoreAppName, viewPath + "Base.ts");

            Utils.CreateFolderForFile(path);
            File.WriteAllText(path, contents);

        }

        public virtual void GenerateAppComponent(string mod)
        {
            string path = _fileNameService.GetComponentFilePath(mod, "app") + ".ts";

            using (Out.Set(ConsoleColor.DarkRed))
                Out.Write(" Ts: ");

            if (!_opts.ReplaceComponentScripts && File.Exists(path))
            {
                WriteColored("Exists", ConsoleColor.Cyan);
                Out.WriteLine();
                return;
            }

            string mainCompBase = _unit.TenantRepository.GetSingleValue(d => d.MainComponentBase, d => d.Code == mod);
            string temp = _molds.MainComponentMold;
            var model = new AppComponentModel
            {
                Name = "AppComponent",
                TemplateName = _fileNameService.ApplyConvension("AppComponent", AppParts.Component),
                BaseComponentName = mainCompBase.GetAfterLast("/") + "Base",
                BaseComponentPath = _fileNameService.GetBaseComponentFilePath(mainCompBase, true)
            };
            string contents = _writer.FillStringParameters(temp, model);

            Utils.CreateFolderForFile(path);
            File.WriteAllText(path, contents);

            WriteSuccess();

        }

        public virtual void GenerateAppModule(string modCode)
        {
            string moduleName = modCode + "Module";
            string modulePath = _fileNameService.GetModuleFilePath(modCode, "app", createFolder: false) + ".ts";

            Out.Write("Generating " + moduleName + ".ts : ");

            if (!_opts.ReplaceMainModule && File.Exists(modulePath))
            {
                GotoColumn(SuccessCol);
                WriteColored("Exists", ConsoleColor.Cyan);
                Out.WriteLine();
                return;
            }


            var main = _unit.TenantRepository.GetSingleValue(d => d.MainComponentBase, d => d.Code == modCode);
            var tempModel = new ModuleTsModel
            {
                Code = "App",
                Modules = "",
                ModuleImports = "",
                MainComponentName = "AppComponent",
                MainComponentPath = _fileNameService.GetComponentImportPath("app", fromDomain: false),
                BaseName = _paths.CoreAppName,
                BaseAppModuleName = _paths.CoreAppName.UCFirst() + "BaseModule",
                BaseAppModulePath = _fileNameService.GetBaseModuleFilePath(true),
                RoutesModulePath = "./" + _fileNameService.ApplyConvension("AppRouting", AppParts.Module)
            };

            var homePage = _unit.PageRepository.GetHomePagePath(modCode);
            if (homePage != null)
            {
                var name = homePage.GetAfterLast("/");
                tempModel.ModuleImports += "import { " + name + " } from '" + _fileNameService.GetComponentImportPath(homePage, false) + "'";
                tempModel.Declarations += name.GetAfterLast("/");
            }

            string moduleTemplate = _molds.AppModuleMold;
            string contents = _writer.FillStringParameters(moduleTemplate, tempModel);
            File.WriteAllText(modulePath, contents);

            GotoColumn(SuccessCol);
            WriteSuccess();
            Out.WriteLine();
        }

        public virtual void GenerateRoutes(string modCode)
        {
            string filePath = _fileNameService.GetModuleFilePath(modCode, "AppRouting", createFolder: false) + ".ts";
            Out.Write($"Generating Routes [{modCode}Routes.ts] : ");

            if (!_opts.ReplaceMainRoutes && File.Exists(filePath))
            {
                GotoColumn(SuccessCol);
                WriteColored("Exists", ConsoleColor.Cyan);
                Out.WriteLine();
                return;
            }

            long modId = _unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);

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

            string homePage = _unit.PageRepository.GetHomePagePath(modCode);
            if (homePage != null)
            {
                var name = homePage.GetAfterLast("/");
                tempModel.ComponentImports += "import { " + name + " } from '" + _fileNameService.GetComponentImportPath(homePage, false) + "'";
                tempModel.Routes += _homeRoute(name);
            }

            foreach (var domain in domains)
            {
                string dom = domain.DomainName;
                tempModel.Routes += _fileNameService.GetDomainLazyLoadingRoute(domain.DomainName) + ",";
            }
            string sep = "";
            foreach (var nav in navs)
            {
                tempModel.DomainsData += sep + _getNavigationObject(nav);
                sep = ",\n\t\t\t";
            }

            _appendLocaleLoaders(tempModel);

            string builder = _writer.FillStringParameters(routesTemplate, tempModel);
            Utils.CreateFolderForFile(filePath);
            File.WriteAllText(filePath, builder);

            GotoColumn(SuccessCol);
            WriteSuccess();
            Out.WriteLine();
        }

        public virtual void GeneratePageCategory(long id)
        {
            string serviceName = null;
            string baseComponent = null;
            bool serviceCreated = false;
            var p = _unit.PageCategoryRepository.FindSingleAs(d => new CategoryBaseComponentDTO
            {
                ViewPath = d.ViewPath,
                Name = d.Name,
                Resource = d.ResourceId != null ? d.Resource.Name : null,
                ResourceDomain = d.ResourceId != null ? d.Resource.Domain.Name : null
            }, d => d.Id == id);

            if (p.Resource != null)
            {
                serviceName = p.Resource + "Service.ts";
                serviceCreated = GenerateHttpService(p.Resource, p.ResourceDomain);
            }

            string baseComponentPath = _fileNameService.GetBaseComponentFilePath(p.ViewPath) + ".ts";
            Utils.CreateFolderForFile(baseComponentPath);

            if (!File.Exists(baseComponentPath))
            {
                GenerateBaseComponent(p.ViewPath);
                baseComponent = p.Name + "Base";
            }

            if (serviceCreated || baseComponent != null)
            {
                Out.WriteLine();
                Out.Write("New files generated : ");
                GotoColumn(6);
                Out.Write("[ ");
                if (serviceCreated)
                {
                    using (Out.Set(ConsoleColor.Yellow))
                        Out.Write(serviceName);
                }

                if (baseComponent != null)
                {
                    if (serviceCreated)
                        Out.Write(" , ");
                    using (Out.Set(ConsoleColor.Cyan))
                        Out.Write(baseComponent + ".ts");
                }
                Out.Write(" ]");
            }

            Out.WriteLine();
        }

        public virtual void GenerateDomainModuleById(string moduleCode, long? domId)
        {
            var doms = new List<DomainWithPagesDTO>();
            if (!_unit.DomainRepository.FindSingleOrAdd(e => e.Id == 1, new Domain { Id = 1, Name = "Shared" }, out Domain shared))
            {
                _unit.SaveChanges();
            }
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
                _generateDomainRecursive(item, moduleCode, parent);
        }

        public virtual void GenerateDomainModule(string moduleCode, string domId)
        {
            long? id = null;
            if (domId != null)
                id = _unit.DomainRepository.GetDomainByPath(domId).Id;
            GenerateDomainModuleById(moduleCode, id);
        }

        public void MoveScript(MovePageRequest r)
        {
            string fromPath = Path.Combine(_paths.UIRoot, r.TenantCode, "app", r.FromPath + ".ts");
            string toPath = Path.Combine(_paths.UIRoot, r.TenantCode, "app", r.ToPath + ".ts");
            if (File.Exists(fromPath))
            {
                Utils.CreateFolderForFile(toPath);
                File.Move(fromPath, toPath);
            }
        }

        public void DeleteScript(string tenantCode, string fromPath)
        {
            string path = Path.Combine(_paths.UIRoot, tenantCode, "app", fromPath + ".ts");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        #region private methods

        protected virtual void _generateDomainRecursive(DomainWithPagesDTO dom, string tenantCode, string parentDomain = null)
        {
            string filePath = _fileNameService.GetModuleFilePath(tenantCode, dom.DomainName, parentDomain) + ".ts";
            Out.Write($"Generating {dom.DomainName}Module : ");
            GotoColumn(SuccessCol);

            if (!_opts.ReplaceDomainRoutes && File.Exists(filePath))
            {
                WriteColored("Exists", ConsoleColor.Cyan);
                dom.Pages = _unit.PageRepository.GetDomainPagesForRouting(tenantCode, dom.Id, true);

                if (!string.IsNullOrEmpty(_paths.LocalizationRoot))
                {
                    var pages = dom.Pages.Select(e => new DataItem { Name = e.PageIdentifier }).ToList();
                    if (dom.Pages.Any())
                        _localization.Import("Pages", Shell.DefaultCulture.TwoLetterISOLanguageName, pages, true);
                }
                Out.WriteLine();
                return;
            }

            bool shared = dom.DomainName == "Shared";
            string template = shared ? _molds.SharedModuleMold : _molds.GetDomainModuleMold();

            dom.Pages = _unit.PageRepository.GetDomainPagesForRouting(tenantCode, dom.Id);

            if (dom.Pages.Any())
            {
                var pages = dom.Pages.Select(e => new DataItem { Name = e.PageIdentifier }).ToList();
                if (!string.IsNullOrEmpty(_paths.LocalizationRoot))
                    _localization.Import("Pages", Shell.DefaultCulture.TwoLetterISOLanguageName, pages, true);
            }

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
                ComponentImports = parentDomain == null ? "" : $"import {{ {parentDomain}Module }} from \"../" + _fileNameService.ApplyConvension(parentDomain, AppParts.Module) + "\";\n",
                Components = "",
                Name = dom.DomainName,
                Registrations = "",
                Routes = "",
                Lazy = "",
                BaseName = _paths.CoreAppName,
                BaseAppModuleName = _paths.CoreAppName.UCFirst() + "BaseModule",
                BaseAppModulePath = _fileNameService.GetBaseModuleFilePath(true),
                PathToRoot = rootPath,
                ParentModules = parentDomain == null ? "" : parentDomain + "Module,"
            };

            foreach (PageDTO p in dom.Pages)
            {
                string component = p.ComponentName;

                model.Components += p.ComponentName + ",";
                model.ComponentImports += $"import {{ {p.ComponentName} }} from '{_fileNameService.GetComponentImportPath(p.Page.ViewPath)}';\n";
                model.Registrations += p.Registration;

                if (p.Page.CanEmbed)
                    model.EmbeddedComponents += p.ComponentName + ", ";
                else if (p.Page.HasRoute)
                    model.Routes += "\t\t\t" + _childRoute(p) + ",\n";
            }

            if (dom.SubDomains != null)
            {
                foreach (DomainWithPagesDTO dp in dom.SubDomains)
                {
                    model.Routes += _fileNameService.GetDomainLazyLoadingRoute(dp.DomainName) + ",";
                }
            }


            string contents = _writer.FillStringParameters(template, model);
            Utils.CreateFolderForFile(filePath);
            File.WriteAllText(filePath, contents);
            WriteSuccess();
            Out.WriteLine();

            if (dom.SubDomains != null && dom.SubDomains.Any())
            {
                foreach (var d in dom.SubDomains)
                    _generateDomainRecursive(d, tenantCode, (parentDomain == null ? "" : parentDomain + "\\") + dom.DomainName);
            }
        }

        private void _appendLocaleLoaders(RoutesTsModel mod)
        {
            if (string.IsNullOrEmpty(_paths.LocalizationRoot))
                return;
            string[] locales = Shell.SupportedLanguages.ToArray();
            foreach (string loc in locales)
            {
                mod.LocalizationImports += "import { " + loc + "_Loader } from \"./localization/" + loc + "/loader\";\n";
                mod.LocalizationLoaders += $"[\"{loc}\"]:new {loc}_Loader, ";
            }
        }

        private string _getNavigationObject(NavigationGroupDTO dto)
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
                    Url = _fileNameService.ApplyConvension(p.Url, AppParts.Route)
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

        private string _homeRoute(string name)
        {
            return $"{{ path: '', component: {name}, data: {{ action: 'anonymous' }} }},\n";
        }

        private string _childRoute(PageDTO p)
        {
            string routeTemplate = _molds.RouteMold;
            string param = p.Page.RouteParameters ?? "";
            string action = p.ActionName == null ? "ResourceActions." + p.Page.PrivilegeType ?? "view" : "\"" + p.ActionName + "\"";
            string component = p.Page.ViewPath.GetAfterLast("/");

            RouteTsModel route = new RouteTsModel
            {
                Path = _fileNameService.ApplyConvension(component, AppParts.Route) + param,

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
