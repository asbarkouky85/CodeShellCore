using CodeShellCore.Cli;
using CodeShellCore.Files;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Angular.Models;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Localization.Services;
using CodeShellCore.Moldster.Models;
using CodeShellCore.Moldster.Pages.Dtos;
using CodeShellCore.Moldster.Pages.Services;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Text;
using CodeShellCore.Text.ResourceReader;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeShellCore.Moldster.Domains.Services
{
    public class DomainScriptGenerationService : ScriptGenerationServiceBase, IDomainScriptGenerationService
    {
        private IMoldProvider _molds => Store.GetInstance<IMoldProvider>();
        private INamingConventionService Names => Store.GetInstance<INamingConventionService>();
        private IPathsService _paths => Store.GetInstance<IPathsService>();
        private IConfigUnit _unit => Store.GetInstance<IConfigUnit>();
        private ILocalizationService _localization => Store.GetInstance<ILocalizationService>();
        public DomainScriptGenerationService(
            IServiceProvider prov,
            IOptions<MoldsterModuleOptions> opt) : base(prov, opt)
        {
        }

        public void GenerateMainFile(string moduleCode, bool addStyle = false)
        {
            string bootPath = Names.GetSrcFolderPath("main-" + Names.ApplyConvension(moduleCode, AppParts.Project), ".ts", keepNameformat: true);
            string pollyPath = Names.GetSrcFolderPath("polyfills");
            string indexPath = Names.GetSrcFolderPath("index", ".html");
            string dec = Names.GetSrcFolderPath("declarations.d");

            if (!File.Exists(bootPath))
            {
                Out.Write("Generating main.ts...  \t\t\t");
                string bootTemplate = _molds.BootMold;
                string boot = Writer.FillStringParameters(bootTemplate, new BootTsModel
                {
                    Code = Names.ApplyConvension(moduleCode, AppParts.Route),
                    ModulePath = Names.ApplyConvension(moduleCode + "/app", AppParts.Route),
                    OtherTenants = _unit.TenantRepository.Exist(e => e.Code != moduleCode)
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

        public virtual void GenerateAppModule(string modCode)
        {
            string moduleName = modCode + "Module";
            string modulePath = Names.GetModuleFilePath(modCode, "app", createFolder: false) + ".ts";

            Out.Write("Generating " + moduleName + ".ts : ");

            if (!Options.ReplaceMainModule && File.Exists(modulePath))
            {
                GotoColumn(SuccessCol);
                WriteColored("Exists", ConsoleColor.Cyan);
                Out.WriteLine();
                return;
            }


            var main = _unit.TenantRepository.GetSingleValue(d => d.MainComponentBase, d => d.Code == modCode);
            var otherTen = _unit.TenantRepository.Exist(e => e.Code != modCode);
            var tempModel = new ModuleTsModel
            {
                Code = "App",
                Modules = "",
                ModuleImports = "",
                MainComponentName = "AppComponent",
                MainComponentPath = Names.GetComponentImportPath("app", fromDomain: false),
                BaseName = _paths.CoreAppName,
                BaseAppModuleName = _paths.CoreAppName.UCFirst() + "BaseModule",
                BaseAppModulePath = Names.GetBaseModuleFilePath(true),
                RoutesModulePath = "./" + Names.ApplyConvension("AppRouting", AppParts.Module),
                BaseHref = otherTen ? "{ provide: APP_BASE_HREF, useValue: '/" + Names.ApplyConvension(modCode, AppParts.Route) + "'}" : ""
            };

            var homePage = _unit.PageRepository.GetHomePagePath(modCode);
            if (homePage != null)
            {
                var name = homePage.GetAfterLast("/");
                tempModel.ModuleImports += "import { " + name + " } from '" + Names.GetComponentImportPath(homePage, false) + "'";
                tempModel.Declarations += name.GetAfterLast("/");
            }

            string moduleTemplate = _molds.AppModuleMold;
            string contents = Writer.FillStringParameters(moduleTemplate, tempModel);
            File.WriteAllText(modulePath, contents);

            GotoColumn(SuccessCol);
            WriteSuccess();
            Out.WriteLine();
        }


        public virtual void GenerateRoutes(string modCode)
        {
            string filePath = Names.GetModuleFilePath(modCode, "AppRouting", createFolder: false) + ".ts";
            Out.Write($"Generating Routes [{modCode}Routes.ts] : ");

            if (!Options.ReplaceMainRoutes && File.Exists(filePath))
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
                tempModel.ComponentImports += "import { " + name + " } from '" + Names.GetComponentImportPath(homePage, false) + "'";
                tempModel.Routes += _homeRoute(name);
            }

            foreach (var domain in domains)
            {
                string dom = domain.DomainName;
                tempModel.Routes += Names.GetDomainLazyLoadingRoute(domain.DomainName) + ",";
            }
            string sep = "";
            foreach (var nav in navs)
            {
                tempModel.DomainsData += sep + _getNavigationObject(nav);
                sep = ",\n\t\t\t";
            }

            _appendLocaleLoaders(tempModel);

            string builder = Writer.FillStringParameters(routesTemplate, tempModel);
            Utils.CreateFolderForFile(filePath);
            File.WriteAllText(filePath, builder);

            GotoColumn(SuccessCol);
            WriteSuccess();
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

        protected virtual void _generateDomainRecursive(DomainWithPagesDTO dom, string tenantCode, string parentDomain = null)
        {
            string filePath = Names.GetModuleFilePath(tenantCode, dom.DomainName, parentDomain) + ".ts";
            Out.Write($"Generating {dom.DomainName}Module : ");
            GotoColumn(SuccessCol);

            if (!Options.ReplaceDomainRoutes && File.Exists(filePath))
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
                ComponentImports = parentDomain == null ? "" : $"import {{ {parentDomain}Module }} from \"../" + Names.ApplyConvension(parentDomain, AppParts.Module) + "\";\n",
                Components = "",
                Name = dom.DomainName,
                Registrations = "",
                Routes = "",
                Lazy = "",
                BaseName = _paths.CoreAppName,
                BaseAppModuleName = _paths.CoreAppName.UCFirst() + "BaseModule",
                BaseAppModulePath = Names.GetBaseModuleFilePath(true),
                PathToRoot = rootPath,
                ParentModules = parentDomain == null ? "" : parentDomain + "Module,"
            };

            foreach (PageDTO p in dom.Pages)
            {
                string component = p.ComponentName;

                model.Components += p.ComponentName + ",";
                model.ComponentImports += $"import {{ {p.ComponentName} }} from '{Names.GetComponentImportPath(p.Page.ViewPath)}';\n";
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
                    model.Routes += Names.GetDomainLazyLoadingRoute(dp.DomainName) + ",";
                }
            }


            string contents = Writer.FillStringParameters(template, model);
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
                    Url = Names.ApplyConvension(p.Url, AppParts.Route)
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
                Path = Names.ApplyConvension(component, AppParts.Route) + param,

                Component = component,
                Name = p.PageIdentifier,
                Action = action,
                Navigate = "false",
                Resource = p.ResourceName,
                Apps = p.Page.Apps == null ? "null" : "[" + p.Page.Apps + "]"
            };
            return Writer.FillStringParameters(routeTemplate, route);
        }
    }
}
