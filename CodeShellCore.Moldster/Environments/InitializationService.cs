using CodeShellCore.Helpers;
using CodeShellCore.Moldster.CodeGeneration.Models;
using CodeShellCore.Moldster.Environments.Services;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Text;
using System;
using System.IO;
using System.IO.Compression;

namespace CodeShellCore.Moldster.Environments
{
    public class InitializationService : MoldsterFileHandlingService, IInitializationService
    {
        public InitializationService(IServiceProvider provider) : base(provider)
        {
        }

        protected virtual void AddBaseModuleFiles(bool replace)
        {
            var serverConfigMold = Molds.GetResourceByNameAsString(MoldNames.ServerConfig_ts);
            string serverConfig = Writer.FillStringParameters(serverConfigMold, new ServerConfigTsModel
            {
                ApiUrl = "http://localhost",
                Production = "false",
                DefaultLocale = Shell.DefaultCulture.TwoLetterISOLanguageName
            });
            AddToBaseFolder(Names.ApplyConvension("ServerConfig", AppParts.Route) + ".ts", serverConfig, true, replace);

            string serverConfigProd = Writer.FillStringParameters(serverConfigMold, new ServerConfigTsModel
            {
                ApiUrl = "",
                Production = "true",
                DefaultLocale = Shell.DefaultCulture.TwoLetterISOLanguageName
            });
            AddToBaseFolder(Names.ApplyConvension("ServerConfig.prod", AppParts.Route) + ".ts", serverConfigProd, true, replace);

            string baseModuleContent = Writer.FillStringParameters(Molds.GetResourceByNameAsString(MoldNames.BaseModule_ts), new DomainTsModel { Name = Paths.CoreAppName.UCFirst() + "Base" });
            AddToBaseFolder(Names.ApplyConvension(Paths.CoreAppName + "BaseModule", AppParts.Module) + ".ts", baseModuleContent, true, replace);
            AddToBaseFolder(Names.ApplyConvension("AppComponent", AppParts.BaseComponent) + ".ts", Molds.GetResourceByNameAsString(MoldNames.AppComponentBase_ts), true, replace);

            AddToBaseFolder(Names.ApplyConvension("Main/Login", AppParts.Component) + ".html", Molds.GetResourceByNameAsString(MoldNames.Login_html), true, replace);
            AddToBaseFolder(Names.ApplyConvension("Main/Login", AppParts.Component) + ".ts", Molds.GetResourceByNameAsString(MoldNames.Login_ts), false, replace);
            AddToBaseFolder(Names.ApplyConvension("Main/topBar", AppParts.Component) + ".html", Molds.GetResourceByNameAsString(MoldNames.TopBar_html), false, replace);
            AddToBaseFolder(Names.ApplyConvension("Main/topBar", AppParts.Component) + ".ts", Molds.GetResourceByNameAsString(MoldNames.TopBar_ts), false, replace);
            AddToBaseFolder(Names.ApplyConvension("Main/navigationSideBar", AppParts.Component) + ".html", Molds.GetResourceByNameAsString(MoldNames.NavigationSideBar_html), false, replace);
            AddToBaseFolder(Names.ApplyConvension("Main/navigationSideBar", AppParts.Component) + ".ts", Molds.GetResourceByNameAsString(MoldNames.NavigationSideBar_ts), false, replace);

            AddToBaseFolder(Names.ApplyConvension("http/AccountService", AppParts.Service) + ".ts", Molds.GetResourceByNameAsString(MoldNames.AccountService_ts), true, replace);
        }

        public virtual void AddShellComponents(bool replace)
        {
            UnZip(Molds.GetResourceByNameAsBytes(MoldNames.ShellComponents_zip), Paths.ConfigRoot, "ShellComponents", replace);
        }

        public virtual void AddUiBasicFiles(bool replace)
        {
            AddToUI("package.json", Molds.GetResourceByNameAsString(MoldNames.Package_json), replace);
            AddToUI("tsconfig.json", Molds.GetResourceByNameAsString(MoldNames.TsConfig_json), replace);
            AddToUI("src/declarations.d.ts", Molds.GetResourceByNameAsString(MoldNames.Declarations_d), replace);
            AddToUI("src/polyfills.ts", Molds.GetResourceByNameAsString(MoldNames.Pollyfills_ts), replace);
            AddToUI("src/index.html", Molds.GetResourceByNameAsString(MoldNames.Index_html), replace);
        }

        public virtual void AddBasicFiles(bool replace)
        {
            AddUiBasicFiles(replace);
            AddBaseModuleFiles(replace);
            string path = Path.Combine(Paths.ConfigRoot, "Views/AppComponent.cshtml");
            if (!File.Exists(path) || replace)
            {
                Utils.CreateFolderForFile(path);
                WriteFileOperation("Adding", "AppComponent.cshtml", true);
                File.WriteAllText(path, Molds.GetResourceByNameAsString(MoldNames.AppComponent_cshtml));
            }
        }

        public virtual void AddCodeShell(bool replace)
        {
            UnZip(Molds.GetResourceByNameAsBytes(MoldNames.CodeShell_zip), Names.CoreFolder, "codeshell", replace);
        }

        public virtual void AddStaticFiles(bool replace)
        {
            string folder = Path.Combine(Paths.UIRoot, "src/assets/moldster");
            UnZip(Molds.GetResourceByNameAsBytes(MoldNames.Css_zip), folder, "css", replace);
            UnZip(Molds.GetResourceByNameAsBytes(MoldNames.Img_zip), folder, "img", replace);
            UnZip(Molds.GetResourceByNameAsBytes(MoldNames.Js_zip), folder, "js", replace);
        }

        protected virtual void UnZip(byte[] bytes, string folder, string name, bool overwrite = false)
        {
            using (var t = SW.Measure())
            {
                string file = Path.Combine(folder, $"{name}.zip");
                string folderPath = Path.Combine(folder, name);
                bool write = true;

                if (Directory.Exists(folderPath))
                {
                    if (overwrite && Utils.DeleteDirectory(folderPath))
                    {
                        write = true;
                    }
                    else
                    {
                        Out.WriteLine(folderPath + " already exists");
                        return;
                    }
                }

                if (write)
                {
                    WriteFileOperation($"Extracting {name} to", folder, false);
                    Utils.CreateFolderForFile(file);
                    File.WriteAllBytes(file, bytes);
                    ZipFile.ExtractToDirectory(file, folderPath);
                    File.Delete(file);
                    WriteSuccess(t.Elapsed, SuccessCol);
                }
                Out.WriteLine();
            }
        }
    }
}
