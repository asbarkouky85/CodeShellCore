using CodeShellCore.Helpers;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.CodeGeneration.Models;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Tenants
{
    public class TenantScriptGenerationService : ScriptGenerationServiceBase, ITenantScriptGenerationService
    {
        protected IMoldProvider Molds => Store.GetRequiredService<IMoldProvider>();
        protected IPathsService Paths => Store.GetRequiredService<IPathsService>();
        protected INamingConventionService Names => Store.GetRequiredService<INamingConventionService>();
        protected IMoldsterUnit _unit => Store.GetRequiredService<IMoldsterUnit>();

        public TenantScriptGenerationService(IServiceProvider provider, IOptions<MoldsterModuleOptions> opts) : base(provider, opts)
        {

        }

        public virtual async Task<Result> AddAngularJson(string tenant)
        {
            var angularJsonPath = Path.Combine(Paths.UIRoot, "angular.json");

            if (!File.Exists(angularJsonPath))
            {
                var ten = Names.ApplyConvension(tenant, AppParts.Project);
                var tenantConfig = Writer.FillStringParameters(Molds.GetResourceByNameAsString(MoldNames.Angular_json_project), new AppComponentModel { Name = ten });
                var angularJsonConent = Writer.FillStringParameters(Molds.GetResourceByNameAsString(MoldNames.Angular_json), new AngularJsonModel { Projects = $"\"{ten}\":" + tenantConfig.Trim(), DefaultProject = ten });
                Utils.CreateFolderForFile(angularJsonPath);
                var str = Encoding.UTF8.GetBytes(angularJsonConent);
                await File.WriteAllBytesAsync(angularJsonPath, str);
            }
            return new Result();
        }

        public virtual async Task GenerateMainFile(string tenantCode, bool addStyle = false)
        {
            string bootPath = Names.GetSrcFolderPath("main-" + Names.ApplyConvension(tenantCode, AppParts.Project), ".ts", keepNameformat: true);
            string pollyPath = Names.GetSrcFolderPath("polyfills");
            string indexPath = Names.GetSrcFolderPath("index", ".html");
            string dec = Names.GetSrcFolderPath("declarations.d");

            if (!File.Exists(bootPath))
            {
                Out.Write("Generating main.ts...  \t\t\t");
                string bootTemplate = Molds.GetResourceByNameAsString(MoldNames.Boot_ts);
                string boot = Writer.FillStringParameters(bootTemplate, new BootTsModel
                {
                    Code = Names.ApplyConvension(tenantCode, AppParts.Route),
                    ModulePath = Names.ApplyConvension(tenantCode + "/app", AppParts.Route),
                    OtherTenants = await _unit.TenantRepository.Exist(e => e.Code != tenantCode)
                });
                File.WriteAllText(bootPath, boot);
                WriteSuccess();
                Out.WriteLine();
            }

            if (!File.Exists(pollyPath))
            {
                Out.Write("Generating polyfills.ts...  \t\t\t");
                string pollyTemplate = Molds.GetResourceByNameAsString(MoldNames.Pollyfills_ts);
                await File.WriteAllTextAsync(pollyPath, pollyTemplate);
                WriteSuccess();
                Out.WriteLine();
            }

            if (!File.Exists(indexPath))
            {
                Out.Write("Generating index.html...  \t\t\t");
                string pollyTemplate = Molds.GetResourceByNameAsString(MoldNames.Index_html);
                await File.WriteAllTextAsync(indexPath, pollyTemplate);
                WriteSuccess();
                Out.WriteLine();
            }

            if (!File.Exists(dec))
            {
                Out.Write("Generating declarations.d.ts...  \t\t\t");
                string pollyTemplate = Molds.GetResourceByNameAsString(MoldNames.Declarations_d);
                await File.WriteAllTextAsync(dec, pollyTemplate);
                WriteSuccess();
                Out.WriteLine();
            }
        }

        public virtual async Task GenerateAppModule(string tenantCode)
        {
            string moduleName = tenantCode + "Module";
            string modulePath = Names.GetModuleFilePath(tenantCode, "app", createFolder: false) + ".ts";

            Out.Write("Generating " + moduleName + ".ts : ");

            if (!Options.ReplaceMainModule && File.Exists(modulePath))
            {
                GotoColumn(SuccessCol);
                WriteColored("Exists", ConsoleColor.Cyan);
                Out.WriteLine();
                return;
            }


            var version = await _unit.TenantRepository.GetSingleValue(d => d.Version, d => d.Code == tenantCode);
            var otherTen = await _unit.TenantRepository.Exist(e => e.Code != tenantCode);
            var tempModel = new ModuleTsModel
            {
                Code = "App",
                Modules = "",
                ModuleImports = "",
                MainComponentName = "AppComponent",
                MainComponentPath = Names.GetComponentImportPath("app", fromDomain: false),
                BaseName = Paths.CoreAppName,
                BaseAppModuleName = Paths.CoreAppName.UCFirst() + "BaseModule",
                BaseAppModulePath = Names.GetBaseModuleFilePath(true),
                Version = version,
                RoutesModulePath = "./" + Names.ApplyConvension("AppRouting", AppParts.Module),
                BaseHref = otherTen ? "{ provide: APP_BASE_HREF, useValue: '/" + Names.ApplyConvension(tenantCode, AppParts.Route) + "'}" : ""
            };

            var homePage = await _unit.PageRepository.GetHomePagePath(tenantCode);
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

        public virtual async Task<AngularJsonFile> ReadAngularJsonFile()
        {
            var angularJsonPath = Path.Combine(Paths.UIRoot, "angular.json");
            var txt = await File.ReadAllTextAsync(angularJsonPath);
            var ob = JObject.Parse(txt);
            return new AngularJsonFile(ob);
        }

        public virtual async Task UpdateAngularJsonFromDatabase()
        {
            var dbTenants = await _unit.TenantRepository.FindAs(e => new { e.Code, e.IsActive });

            var angularTenants = await ReadAngularJsonFile();

            foreach (var t in dbTenants)
            {
                var ten = Names.ApplyConvension(t.Code, AppParts.Project);
                if (!angularTenants.Tenants.TryGetValue(ten, out AngularTenant ngt))
                {
                    var tenantConfig = Writer.FillStringParameters(Molds.GetResourceByNameAsString(MoldNames.Angular_json_project), new AppComponentModel { Name = t.Code });
                    angularTenants.AddTenant(ten, tenantConfig);
                }
            }
            var txt = angularTenants.JObject.ToString(Formatting.Indented);
            File.WriteAllText(Path.Combine(Paths.UIRoot, "angular.json"), txt);
        }
    }
}
