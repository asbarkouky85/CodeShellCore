using CodeShellCore.Data.Sql;
using CodeShellCore.Helpers;
using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.CodeGeneration.Models;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Moldster.Environments.Services;
using CodeShellCore.Moldster.Resources.Services;
using CodeShellCore.Moldster.Tenants.Services;
using CodeShellCore.Text;
using System;
using System.IO;

namespace CodeShellCore.Moldster.Services
{
    public class MigrationService : MoldsterFileHandlingService, IMigrationService
    {
        IConfigUnit unit => GetService<IConfigUnit>();
        IResourceScriptGenerationService resTs => GetService<IResourceScriptGenerationService>();

        IInitializationService Init => GetService<IInitializationService>();
        ITenantScriptGenerationService TenantTs => GetService<ITenantScriptGenerationService>();
        public MigrationService(IServiceProvider provider) : base(provider)
        {
        }

        public void AddMigrationsTable()
        {
            var sql = GetService<ISqlCommandService>();
            var env = GetService<EnvironmentAccessor>();

            env.CurrentEnvironment = new MoldsterEnvironment
            {
                ConnectionParams = new DbConnectionParams(Shell.GetConfigAs<string>("ConnectionStrings:Moldster"))
            };
            WriteColored("ConnectionString=" + env.CurrentEnvironment.ConnectionParams.ConnectionString, ConsoleColor.Yellow);
            sql.AddMigrationTable();
        }

        public Result MigrateBaseModule(string tenant)
        {
            AddMigrationsTable();
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
                    resTs.GenerateHttpService(srv.Name, srv.DomainName);

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

            if (Directory.Exists(oldAssetsPath))
            {
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
            }

            Utils.DeleteEmptyDirectories(oldAssetsPath);

            var packing = Directory.GetFiles(Paths.UIRoot, "*webpack*");
            foreach (var f in packing)
                File.Delete(f);

            Utils.DeleteDirectory(Path.Combine(Paths.UIRoot, "Core/codeshell"));
            Init.AddCodeShell(true);
            Init.AddUiBasicFiles(true);
            TenantTs.AddAngularJson(tenant);
            TenantTs.UpdateAngularJsonFromDatabase();

            string bootTemplate = Molds.GetResourceByNameAsString(MoldNames.Boot_ts);
            string boot = Writer.FillStringParameters(bootTemplate, new BootTsModel
            {
                Code = Names.ApplyConvension(tenant, AppParts.Route),
                ModulePath = Names.ApplyConvension(tenant + "/app", AppParts.Module),
                OtherTenants = unit.TenantRepository.Exist(e => e.Code != tenant)
            });
            string bootPath = Names.GetSrcFolderPath("main-" + Names.ApplyConvension(tenant, AppParts.Project), ".ts", keepNameformat: true);
            File.WriteAllText(bootPath, boot);
            return new Result();
        }
    }
}
