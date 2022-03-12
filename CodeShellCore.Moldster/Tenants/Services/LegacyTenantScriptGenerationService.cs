using CodeShellCore.Helpers;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.CodeGeneration.Dtos;
using CodeShellCore.Moldster.CodeGeneration.Models;
using CodeShellCore.Text;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeShellCore.Moldster.Tenants.Services
{
    public class LegacyTenantScriptGenerationService : TenantScriptGenerationService
    {
        public LegacyTenantScriptGenerationService(IServiceProvider provider, IOptions<MoldsterModuleOptions> opts) : base(provider, opts)
        {
        }

        public override Result AddAngularJson(string tenant)
        {
            return new Result();
        }

        public override AngularJsonFile ReadAngularJsonFile()
        {
            return new AngularJsonFile();
        }

        public override void UpdateAngularJsonFromDatabase()
        {
            
        }

        public override void GenerateMainFile(string tenantCode, bool addStyle = false)
        {
            Out.Write("Generating boot.ts...  \t\t\t");

            string bootPath = Path.Combine(Paths.UIRoot, tenantCode, "boot.ts");
            string bootTemplate = Molds.GetResourceByNameAsString(MoldNames.Boot_ts);

            string boot = Writer.FillStringParameters(bootTemplate, new BootTsModel
            {
                Code = tenantCode,
                Style = addStyle ? "import \"./app.scss\"" : ""
            });
            File.WriteAllText(bootPath, boot);

            GotoColumn(SuccessCol);
            WriteSuccess();
            Out.WriteLine();
        }

        public override void GenerateAppModule(string modCode)
        {
            string moduleName = modCode + "Module";
            string modulePath = Path.Combine(Paths.UIRoot, modCode, "app", moduleName + ".ts");

            Out.Write("Generating " + moduleName + ".ts : ");
            var main = _unit.TenantRepository.GetSingleValue(d => d.MainComponentBase, d => d.Code == modCode);
            var tempModel = new ModuleTsModel
            {
                Code = modCode,
                Modules = "",
                ModuleImports = "",
                MainComponentName = main,
                MainComponentPath = "./" + main,
                BaseName = Paths.CoreAppName,
                BaseAppModuleName = Paths.CoreAppName + "BaseModule",
                BaseAppModulePath = Paths.CoreAppName + "/" + Paths.CoreAppName + "BaseModule"
            };

            var homePage = _unit.PageRepository.GetHomePagePath(modCode);
            if (homePage != null)
            {
                var name = homePage.GetAfterLast("/");
                tempModel.ModuleImports += "import { " + name + " } from '" + Names.GetComponentImportPath(homePage, false) + "'";
                tempModel.Declarations += name;
            }

            string moduleTemplate = Molds.GetResourceByNameAsString(MoldNames.Module_ts);
            string contents = Writer.FillStringParameters(moduleTemplate, tempModel);
            File.WriteAllText(modulePath, contents);

            GotoColumn(SuccessCol);
            WriteSuccess();
            Out.WriteLine();
        }
    }
}
