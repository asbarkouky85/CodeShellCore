using CodeShellCore.Data.Mapping;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.CodeGeneration.Models;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Moldster.Localization;
using CodeShellCore.Moldster.Navigation;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Text;
using CodeShellCore.Text.ResourceReader;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeShellCore.Moldster.Domains
{
    public class DomainScriptGenerationService : ScriptGenerationServiceBase, IDomainScriptGenerationService
    {
        protected IMoldProvider _molds => Store.GetInstance<IMoldProvider>();
        protected INamingConventionService Names => Store.GetInstance<INamingConventionService>();
        protected IPathsService _paths => Store.GetInstance<IPathsService>();
        protected IConfigUnit _unit => Store.GetInstance<IConfigUnit>();
        protected ILocalizationService _localization => Store.GetInstance<ILocalizationService>();
        protected IObjectMapper Mapper;
        public DomainScriptGenerationService(
            IServiceProvider prov,
            IOptions<MoldsterModuleOptions> opt) : base(prov, opt)
        {
            Mapper = Store.GetInstance<IObjectMapper>();
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

            long tenantId = _unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);

            IEnumerable<DomainDto> domains = _unit.DomainRepository.GetParentModules<DomainDto>(tenantId);
            IEnumerable<NavigationGroupDTO> navs = _unit.NavigationGroupRepository.GetTenantNavs<NavigationGroupDTO>(tenantId);

            string routesTemplate = _molds.GetResourceByNameAsString(MoldNames.Routes_ts);//RoutesMold;

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
                tempModel.Routes += HomeRoute(name);
            }

            foreach (var domain in domains)
            {
                string dom = domain.DomainName;
                tempModel.Routes += Names.GetDomainLazyLoadingRoute(domain.DomainName) + ",";
            }

            string sep = "";
            foreach (var nav in navs)
            {
                var pages = _unit.NavigationPageRepository.FindAndMap<NavigationPageDTO>(e => e.Page.TenantId == tenantId && e.NavigationGroupId == nav.Id);
                tempModel.DomainsData += sep + GetNavigationObject(nav.Name, pages);
                sep = ",\n\t\t\t";
            }

            AppendLocaleLoaders(tempModel);

            string builder = Writer.FillStringParameters(routesTemplate, tempModel);
            Utils.CreateFolderForFile(filePath);
            File.WriteAllText(filePath, builder);

            GotoColumn(SuccessCol);
            WriteSuccess();
            Out.WriteLine();
        }

        public virtual void GenerateDomainModuleById(string moduleCode, long? domId)
        {
            var doms = new List<DomainDto>();
            if (!_unit.DomainRepository.FindSingleOrAdd(e => e.Id == 1, new Domain { Id = 1, Name = "Shared" }, out Domain shared))
            {
                _unit.SaveChanges();
            }
            doms = _unit.DomainRepository.GetByTenantCodeForRouting<DomainDto>(moduleCode, domId);

            var newList = new List<DomainDto>();
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

        protected virtual void _generateDomainRecursive(DomainDto dom, string tenantCode, string parentDomain = null)
        {
            string filePath = Names.GetModuleFilePath(tenantCode, dom.DomainName, parentDomain) + ".ts";
            Out.Write($"Generating {dom.DomainName}Module : ");
            GotoColumn(SuccessCol);

            if (!Options.ReplaceDomainRoutes && File.Exists(filePath))
            {
                WriteColored("Exists", ConsoleColor.Cyan);
                var domPages = _unit.PageRepository.GetDomainPagesForRouting<PageDetailsDto>(tenantCode, dom.Id, true);

                if (!string.IsNullOrEmpty(_paths.LocalizationRoot))
                {
                    var pages = domPages.Select(e => new DataItem { Name = e.PageIdentifier }).ToList();
                    if (domPages.Any())
                        _localization.Import("Pages", Shell.DefaultCulture.TwoLetterISOLanguageName, pages, true);
                }
                Out.WriteLine();
                return;
            }

            bool shared = dom.DomainName == "Shared";
            string template = shared ? _molds.GetResourceByNameAsString(MoldNames.SharedModule_ts) : _molds.GetDomainModuleMold();

            var domainPages = _unit.PageRepository.GetDomainPagesForRouting<PageDetailsDto>(tenantCode, dom.Id);

            if (domainPages.Any())
            {
                var pages = domainPages.Select(e => new DataItem { Name = e.PageIdentifier }).ToList();
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

            foreach (PageDetailsDto p in domainPages)
            {
                string component = p.ComponentName;

                model.Components += p.ComponentName + ",";
                model.ComponentImports += $"import {{ {p.ComponentName} }} from '{Names.GetComponentImportPath(p.Page.ViewPath)}';\n";
                model.Registrations += p.Registration;

                if (p.Page.CanEmbed)
                    model.EmbeddedComponents += p.ComponentName + ", ";
                else if (p.Page.HasRoute)
                    model.Routes += "\t\t\t" + ChildRoute(p) + ",\n";
            }

            if (dom.SubDomains != null)
            {
                foreach (DomainDto dp in dom.SubDomains)
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

        protected virtual void AppendLocaleLoaders(RoutesTsModel mod)
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

        protected string GetNavigationObject(string groupName, IEnumerable<NavigationPageDTO> pages)
        {
            string children = "";
            foreach (var p in pages)
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
            return string.Format("{{\n\t\t\t\tname: \"{0}\" ,\n\t\t\t\tchildren: [{1}]\n\t\t\t}}", groupName, children);
        }

        protected string HomeRoute(string name)
        {
            return $"{{ path: '', component: {name}, data: {{ action: 'anonymous' }} }},\n";
        }

        protected string ChildRoute(PageDetailsDto p)
        {
            string routeTemplate = _molds.GetResourceByNameAsString(MoldNames.Route_ts);
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
