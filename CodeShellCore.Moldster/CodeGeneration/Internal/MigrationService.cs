using CodeShellCore.Cli;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Angular.Models;
using CodeShellCore.Moldster.Builder;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeShellCore.Moldster.CodeGeneration.Internal
{
    public class MigrationService : MoldsterFileHandlingService, IMigrationService
    {
        IConfigUnit unit => GetService<IConfigUnit>();
        IScriptGenerationService script => GetService<IScriptGenerationService>();

        IBundlingService Bundling => GetService<IBundlingService>();
        IBuilderService Builder => GetService<IBuilderService>();
        public MigrationService(IServiceProvider provider) : base(provider)
        {
        }

        public Result MigrateBaseModule(string tenant)
        {
            var oldBasePath = Path.Combine(Paths.UIRoot, "Core", Paths.CoreAppName);

            if (Directory.Exists(oldBasePath))
            {
                var bas = unit.PageCategoryRepository.FindAs(e => e.ViewPath);


                foreach (var ba in bas)
                {
                    var f = Path.Combine(oldBasePath, ba + "Base.ts");
                    if (File.Exists(f))
                    {
                        var path = Names.GetBaseComponentFilePath(ba) + ".ts";
                        Utils.CreateFolderForFile(path);
                        if (!File.Exists(path))
                        {

                            var content = File.ReadAllBytes(f);
                            File.WriteAllBytes(path, content);
                            Out.WriteLine("Added " + path);
                        }
                        File.Delete(f);
                    }
                }

                var srvs = unit.ResourceRepository.FindAs(e => new { e.Name, DomainName = e.Domain.Name });
                foreach (var srv in srvs)
                {
                    script.GenerateHttpService(srv.Name, srv.DomainName);

                    var dom = (srv.DomainName != null ? srv.DomainName + "\\" : "") + "Http";
                    var f = Path.Combine(oldBasePath, dom, srv.Name + "Service.ts");
                    if (File.Exists(f))
                    {
                        var folder = Names.GetHttpServiceFolder(srv.DomainName);
                        string servicePath = Path.Combine(Paths.UIRoot, folder + "/" + Names.ApplyConvension(srv.Name, AppParts.Service) + ".ts");

                        if (File.Exists(servicePath))
                        {
                            var content = File.ReadAllBytes(f);
                            File.WriteAllBytes(servicePath, content);
                            Out.WriteLine("Added " + servicePath);
                        }
                        File.Delete(f);
                    }
                }

                var bs = Paths.CoreAppName + "BaseModule";
                var baseModule = Path.Combine(oldBasePath, bs + ".ts");
                if (File.Exists(baseModule))
                {
                    var conf = Names.ApplyConvension(bs, AppParts.Module);
                    var content = File.ReadAllText(baseModule);
                    AddToBaseFolder(conf + ".ts", content, true);
                    File.Delete(baseModule);
                }
                var appComponent = Path.Combine(oldBasePath, "AppComponentBase.ts");
                if (File.Exists(appComponent))
                {
                    var conf = Names.ApplyConvension("AppComponentBase", AppParts.Component);
                    var content = File.ReadAllText(appComponent);
                    AddToBaseFolder(conf + ".ts", content, true);
                    File.Delete(appComponent);
                }


                var main = Path.Combine(oldBasePath, "Main");
                if (Directory.Exists(main))
                {
                    var mainFiles = Directory.GetFiles(main, "*.ts");
                    foreach (var fl in mainFiles)
                    {
                        var name = fl.Replace("\\", "/").GetAfterLast("/").GetBeforeFirst(".");
                        var htmlFile = Path.Combine(main, name + ".html");
                        var tsFile = Path.Combine(main, name + ".ts");

                        if (File.Exists(htmlFile) && File.Exists(tsFile))
                        {
                            var htmlContent = File.ReadAllText(htmlFile);
                            var tsContent = File.ReadAllText(tsFile);
                            AddToBaseFolder(Names.ApplyConvension("main/" + name, AppParts.Component) + ".html", htmlContent, true);
                            AddToBaseFolder(Names.ApplyConvension("main/" + name, AppParts.Component) + ".ts", tsContent, true);

                            File.Delete(tsFile);
                            File.Delete(htmlFile);
                        }
                    }

                }
                var fls = Directory.GetFiles(oldBasePath, "*.ts", SearchOption.AllDirectories);
                foreach (var fl in fls)
                {
                    var name = fl.Replace(oldBasePath + "\\", "").Replace("\\", "/").GetBeforeFirst(".");
                    var tsFile = Path.Combine(oldBasePath, name + ".ts");
                    var part = AppParts.Route;
                    if (File.Exists(tsFile))
                    {
                        var tsContent = File.ReadAllText(tsFile);

                        if (name.ToLower().Contains("service"))
                        {
                            part = AppParts.Service;
                            name = name.Replace("Service", "");
                        }
                        AddToBaseFolder(Names.ApplyConvension(name, part) + ".ts", tsContent, true);
                        File.Delete(tsFile);
                    }
                }
                Utils.DeleteEmptyDirectories(oldBasePath);
            }


            var oldAssetsPath = Path.Combine(Paths.UIRoot, "wwwroot");
            Utils.DeleteDirectory(Path.Combine(Paths.UIRoot, "wwwroot/dist"));

            var assets = Directory.GetFiles(oldAssetsPath, "*", SearchOption.AllDirectories);
            var assetsTar = Path.Combine(Paths.UIRoot, "src/assets");
            foreach (var fl in assets)
            {
                if (!fl.Contains("\\dist\\"))
                {
                    var newPath = fl.Replace(oldAssetsPath, assetsTar);
                    Utils.CreateFolderForFile(newPath);
                    File.Move(fl, newPath);
                }
            }

            
            Utils.DeleteEmptyDirectories(oldAssetsPath);

            var packing = Directory.GetFiles(Paths.UIRoot, "*webpack*");
            foreach (var f in packing)
                File.Delete(f);

            Utils.DeleteDirectory(Path.Combine(Paths.UIRoot, "Core/codeshell"));
            Bundling.AddCodeShell(true);
            Bundling.GenerateTsEnvironment(true);
            Builder.AddTenantToAngularJson(tenant);

            string bootTemplate = Molds.BootMold;
            string boot = Writer.FillStringParameters(bootTemplate, new BootTsModel
            {
                Code = tenant.UCFirst(),
                ModulePath = Names.ApplyConvension(tenant + "/app", AppParts.Module)
            });
            string bootPath = Names.GetSrcFolderPath("main-" + tenant, ".ts", keepNameformat: true);
            File.WriteAllText(bootPath, boot);
            return new Result();
        }
    }
}
