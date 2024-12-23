using CodeShellCore.Helpers;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.CodeGeneration.Models;
using CodeShellCore.Text;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Tenants
{
    public class LegacyTenantScriptGenerationService : TenantScriptGenerationService
    {
        public LegacyTenantScriptGenerationService(IServiceProvider provider, IOptions<MoldsterModuleOptions> opts) : base(provider, opts)
        {
        }

        public override Task<Result> AddAngularJson(string tenant)
        {
            return Task.FromResult(new Result());
        }

        public override Task<AngularJsonFile> ReadAngularJsonFile()
        {
            return Task.FromResult(new AngularJsonFile());
        }

        public override Task UpdateAngularJsonFromDatabase()
        {
            return Task.CompletedTask;
        }

        public override async Task GenerateMainFile(string tenantCode, bool addStyle = false)
        {
            Out.Write("Generating boot.ts...  \t\t\t");

            string bootPath = Path.Combine(Paths.UIRoot, tenantCode, "boot.ts");
            string bootTemplate = Molds.GetResourceByNameAsString(MoldNames.Boot_ts);

            string boot = Writer.FillStringParameters(bootTemplate, new BootTsModel
            {
                Code = tenantCode,
                Style = addStyle ? "import \"./app.scss\"" : ""
            });
            await File.WriteAllTextAsync(bootPath, boot);

            GotoColumn(SuccessCol);
            WriteSuccess();
            Out.WriteLine();
        }

        public override async Task GenerateAppModule(string modCode)
        {
            string moduleName = modCode + "Module";
            string modulePath = Path.Combine(Paths.UIRoot, modCode, "app", moduleName + ".ts");

            Out.Write("Generating " + moduleName + ".ts : ");
            var main = await _unit.TenantRepository.GetSingleValue(d => d.MainComponentBase, d => d.Code == modCode);
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

            var homePage = await _unit.PageRepository.GetHomePagePath(modCode);
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
