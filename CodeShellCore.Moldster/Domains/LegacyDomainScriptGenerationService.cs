using CodeShellCore.Moldster.CodeGeneration.Models;
using CodeShellCore.Moldster.Navigation;
using CodeShellCore.Text;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Domains
{
    public class LegacyDomainScriptGenerationService : DomainScriptGenerationService
    {
        public LegacyDomainScriptGenerationService(IServiceProvider prov, IOptions<MoldsterModuleOptions> opt) : base(prov, opt)
        {
        }

        public override async Task GenerateRoutes(string modCode)
        {
            long tenantId = await _unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);
            string fileName = modCode + "Routes";
            string filePath = Path.Combine(_paths.UIRoot, modCode, "app", fileName + ".ts");

            IEnumerable<DomainModuleDto> domains = await _unit.DomainRepository.GetParentModules<DomainModuleDto>(tenantId);
            IEnumerable<NavigationGroupLookupDto> navs = await _unit.NavigationGroupRepository.GetTenantNavs<NavigationGroupLookupDto>(tenantId);

            string routesTemplate = _molds.GetResourceByNameAsString(MoldNames.Routes_ts);

            var tempModel = new RoutesTsModel
            {
                LocalizationImports = "",
                ComponentImports = "",
                LocalizationLoaders = "",
                Routes = "",
                Registration = "",
                BaseName = _paths.CoreAppName
            };

            string home = await _unit.PageRepository.GetHomePagePath(modCode);
            if (home != null)
            {
                var name = home.GetAfterLast("/");
                tempModel.ComponentImports += "import { " + name + " } from '" + Names.GetComponentImportPath(home, false) + "'";
                tempModel.Routes += HomeRoute(name);
            }

            foreach (var domain in domains)
            {
                string dom = domain.DomainName;
                tempModel.Routes += Names.GetDomainLazyLoadingRoute(domain.DomainName) + ",\r\n\t";
            }
            string sep = "";
            foreach (var nav in navs)
            {
                var pages = await _unit.NavigationPageRepository.FindAndMap<NavigationPageRouteDto>(e => e.Page.TenantId == tenantId && e.NavigationGroupId == nav.Id);
                tempModel.DomainsData += sep + GetNavigationObject(nav.Name, pages);
                sep = ",\n\t\t\t";
            }

            AppendLocaleLoaders(tempModel);

            string builder = Writer.FillStringParameters(routesTemplate, tempModel);
            File.WriteAllText(filePath, builder);
        }

        protected override void AppendLocaleLoaders(RoutesTsModel mod)
        {


            if (string.IsNullOrEmpty(_paths.LocalizationRoot))
                return;
            string[] locales = Shell.SupportedLanguages.ToArray();
            foreach (string loc in locales)
            {
                mod.LocalizationImports += "import { " + loc + "_Loader } from \"./../Localization/" + loc + "/loader\";\n";
                mod.LocalizationLoaders += $"[\"{loc}\"]:new {loc}_Loader, ";
            }

        }
    }
}
