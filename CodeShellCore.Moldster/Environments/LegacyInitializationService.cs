using CodeShellCore.Helpers;
using CodeShellCore.Moldster.CodeGeneration.Models;
using CodeShellCore.Moldster.Environments.Services;
using System;
using System.IO;

namespace CodeShellCore.Moldster.Environments
{
    public class LegacyInitializationService : InitializationService, IInitializationService
    {
        public LegacyInitializationService(IServiceProvider provider) : base(provider)
        {
        }

        public override void AddStaticFiles(bool replace)
        {
            string folder = Path.Combine(Paths.UIRoot, "wwwroot");
            UnZip(Molds.GetResourceByNameAsBytes(MoldNames.Css_zip), folder, "css", replace);
            UnZip(Molds.GetResourceByNameAsBytes(MoldNames.Img_zip), folder, "img", replace);
            UnZip(Molds.GetResourceByNameAsBytes(MoldNames.Js_zip), folder, "js", replace);
        }

        public override void AddCodeShell(bool replace)
        {
            string folder = Path.Combine(Paths.UIRoot, "Core");
            UnZip(Molds.GetResourceByNameAsBytes(MoldNames.CodeShell_zip), folder, "codeshell", replace);
        }

        protected override void AddBaseModuleFiles(bool replace)
        {
            string content = Writer.FillStringParameters(
                Molds.GetResourceByNameAsString(MoldNames.BaseModule_ts),
                new DomainTsModel
                {
                    Name = Paths.CoreAppName + "Base"
                });

            AddToBaseFolder(Paths.CoreAppName + "BaseModule.ts", content, true, replace);
            AddToBaseFolder("AppComponentBase.ts", Molds.GetResourceByNameAsString(MoldNames.AppComponentBase_ts), true, replace);
            AddToBaseFolder("ServerConfig.ts", Molds.GetResourceByNameAsString(MoldNames.ServerConfig_ts), replace);

            AddToBaseFolder("Main/Login.html", Molds.GetResourceByNameAsString(MoldNames.Login_html), true, replace);
            AddToBaseFolder("Main/Login.ts", Molds.GetResourceByNameAsString(MoldNames.Login_ts), false, replace);
            AddToBaseFolder("Main/topBar.html", Molds.GetResourceByNameAsString(MoldNames.TopBar_html), false, replace);
            AddToBaseFolder("Main/topBar.ts", Molds.GetResourceByNameAsString(MoldNames.TopBar_ts), false, replace);
            AddToBaseFolder("Main/navigationSideBar.html", Molds.GetResourceByNameAsString(MoldNames.NavigationSideBar_html), false, replace);
            AddToBaseFolder("Main/navigationSideBar.ts", Molds.GetResourceByNameAsString(MoldNames.NavigationSideBar_ts), false, replace);

            AddToBaseFolder("Http/AccountService.ts", Molds.GetResourceByNameAsString(MoldNames.AccountService_ts), true, replace);
        }

        public override void AddUiBasicFiles(bool replace)
        {
            AddToUI("package.json", Molds.GetResourceByNameAsString(MoldNames.Package_json), replace);
            AddToUI("tsconfig.json", Molds.GetResourceByNameAsString(MoldNames.TsConfig_json), replace);
            AddToUI("declarations.d.ts", Molds.GetResourceByNameAsString(MoldNames.Declarations_d), replace);
            AddToUI("WebPackSharedConfig.js", Molds.GetResourceByNameAsString(MoldNames.WebPackSharedConfig_js), replace);
            AddToUI("webpack.config.vendor.js", Molds.GetResourceByNameAsString(MoldNames.WebPackConfigVendor_js), replace);
            AddToUI("appsettings.development.json", Molds.GetResourceByNameAsString(MoldNames.AppSettings_json), replace);
        }

        public override void AddBasicFiles(bool replace)
        {
            AddBaseModuleFiles(replace);
            AddUiBasicFiles(replace);

            var path = Path.Combine(Paths.UIRoot, "Pages/Index.cshtml");
            if (!File.Exists(path) || replace)
            {
                Utils.CreateFolderForFile(path);
                WriteFileOperation("Adding", "Index.cshtml", true);
                File.WriteAllText(path, Molds.GetResourceByNameAsString(MoldNames.Index_cshtml));
            }

            path = Path.Combine(Paths.ConfigRoot, "Views/AppComponent.cshtml");
            if (!File.Exists(path) || replace)
            {
                Utils.CreateFolderForFile(path);
                WriteFileOperation("Adding", "AppComponent.cshtml", true);
                File.WriteAllText(path, Molds.GetResourceByNameAsString(MoldNames.AppComponent_cshtml));
            }
        }
    }
}
