using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Angular.Models;
using CodeShellCore.Moldster.Models;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace CodeShellCore.Moldster.Environments.Services
{
    public class InitializationService : MoldsterFileHandlingService, IInitializationService
    {
        public InitializationService(IServiceProvider provider) : base(provider)
        {
        }

        protected void AddBaseModuleFiles(bool replace)
        {
            string serverConfig = Writer.FillStringParameters(Molds.ServerConfigMold, new ServerConfigTsModel
            {
                ApiUrl = "http://localhost",
                Production = "false",
                DefaultLocale = Shell.DefaultCulture.TwoLetterISOLanguageName
            });
            AddToBaseFolder(Names.ApplyConvension("ServerConfig", AppParts.Route) + ".ts", serverConfig, true, replace);

            string serverConfigProd = Writer.FillStringParameters(Molds.ServerConfigMold, new ServerConfigTsModel
            {
                ApiUrl = "",
                Production = "true",
                DefaultLocale = Shell.DefaultCulture.TwoLetterISOLanguageName
            });
            AddToBaseFolder(Names.ApplyConvension("ServerConfig.prod", AppParts.Route) + ".ts", serverConfigProd, true, replace);

            string baseModuleContent = Writer.FillStringParameters(Molds.BaseModuleMold, new DomainTsModel { Name = Paths.CoreAppName.UCFirst() + "Base" });
            AddToBaseFolder(Names.ApplyConvension(Paths.CoreAppName + "BaseModule", AppParts.Module) + ".ts", baseModuleContent, true, replace);
            AddToBaseFolder(Names.ApplyConvension("AppComponent", AppParts.BaseComponent) + ".ts", Properties.Resources.AppComponentBase_ts, true, replace);

            AddToBaseFolder(Names.ApplyConvension("Main/Login", AppParts.Component) + ".html", Properties.Resources.Login_html, true, replace);
            AddToBaseFolder(Names.ApplyConvension("Main/Login", AppParts.Component) + ".ts", Properties.Resources.Login_ts, false, replace);
            AddToBaseFolder(Names.ApplyConvension("Main/topBar", AppParts.Component) + ".html", Properties.Resources.topBar_html, false, replace);
            AddToBaseFolder(Names.ApplyConvension("Main/topBar", AppParts.Component) + ".ts", Properties.Resources.topBar_ts, false, replace);
            AddToBaseFolder(Names.ApplyConvension("Main/navigationSideBar", AppParts.Component) + ".html", Properties.Resources.navigationSideBar_html, false, replace);
            AddToBaseFolder(Names.ApplyConvension("Main/navigationSideBar", AppParts.Component) + ".ts", Properties.Resources.navigationSideBar_ts, false, replace);

            AddToBaseFolder(Names.ApplyConvension("http/AccountService", AppParts.Service) + ".ts", Properties.Resources.AccountService_ts, true, replace);
        }

        public void AddShellComponents(bool replace)
        {
            UnZip(Properties.Resources.ShellComponents, Paths.ConfigRoot, "ShellComponents", replace);
        }

        public void AddUiBasicFiles(bool replace)
        {
            AddToUI("package.json", Properties.Resources.package_json, replace);
            AddToUI("tsconfig.json", Properties.Resources.tsconfig_json, replace);
            AddToUI("src/declarations.d.ts", Properties.Resources.declarations_d, replace);
            AddToUI("src/polyfills.ts", Properties.Resources.pollyfills_ts, replace);
            AddToUI("src/index.html", Properties.Resources.index_html, replace);
        }

        public void AddBasicFiles(bool replace)
        {
            AddUiBasicFiles(replace);
            AddBaseModuleFiles(replace);
            string path = Path.Combine(Paths.ConfigRoot, "Views/AppComponent.cshtml");
            if (!File.Exists(path) || replace)
            {
                Utils.CreateFolderForFile(path);
                WriteFileOperation("Adding", "AppComponent.cshtml", true);
                File.WriteAllText(path, Properties.Resources.AppComponent_cshtml);
            }
        }

        public void AddCodeShell(bool replace)
        {
            UnZip(Properties.Resources.codeshell, Names.CoreFolder, "codeshell", replace);
        }

        public virtual void AddStaticFiles(bool replace)
        {
            string folder = Path.Combine(Paths.UIRoot, "src/assets/moldster");
            UnZip(Properties.Resources.css, folder, "css", replace);
            UnZip(Properties.Resources.img, folder, "img", replace);
            UnZip(Properties.Resources.js, folder, "js", replace);
        }

        protected void UnZip(byte[] bytes, string folder, string name, bool overwrite = false)
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
