using CodeShellCore.Moldster.CodeGeneration.Models;
using CodeShellCore.Moldster.Navigation.Dtos;
using CodeShellCore.Text;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeShellCore.Moldster.Domains
{
    public class LegacyDomainScriptGenerationService : DomainScriptGenerationService
    {
        public LegacyDomainScriptGenerationService(IServiceProvider prov, IOptions<MoldsterModuleOptions> opt) : base(prov, opt)
        {
        }

        public override void GenerateRoutes(string modCode)
        {
            long modId = _unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == modCode);
            string fileName = modCode + "Routes";
            string filePath = Path.Combine(_paths.UIRoot, modCode, "app", fileName + ".ts");

            IEnumerable<DomainWithPagesDTO> domains = _unit.DomainRepository.GetParentModules(modId);
            IEnumerable<NavigationGroupDTO> navs = _unit.NavigationGroupRepository.GetTenantNavs(modId);
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

            string home = _unit.PageRepository.GetHomePagePath(modCode);
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
                tempModel.DomainsData += sep + GetNavigationObject(nav);
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
