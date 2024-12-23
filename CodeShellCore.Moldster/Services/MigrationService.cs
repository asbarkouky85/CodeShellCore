using CodeShellCore.Data.Sql;
using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Moldster.CodeGeneration.Models;
using CodeShellCore.Moldster.Domains.DataObjects;
using CodeShellCore.Moldster.Environments;
using CodeShellCore.Moldster.Environments.Services;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Resources;
using CodeShellCore.Moldster.Sql;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Services
{
    public class MigrationService : MoldsterFileHandlingService, IMigrationService
    {
        IMoldsterUnit unit => GetService<IMoldsterUnit>();
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

        public async Task<Result> MigrateBaseModule(string tenant)
        {
            AddMigrationsTable();
            var oldBasePath = Path.Combine(Paths.UIRoot, "Core", Paths.CoreAppName);

            if (Directory.Exists(oldBasePath))
            {
                var bas = await unit.PageCategoryRepository.FindAs(e => e.ViewPath);

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

                var srvs = await unit.ResourceRepository.FindAs(e => new { e.Name, DomainName = e.Domain.Name });
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
            await TenantTs.AddAngularJson(tenant);
            await TenantTs.UpdateAngularJsonFromDatabase();

            string bootTemplate = Molds.GetResourceByNameAsString(MoldNames.Boot_ts);
            string boot = Writer.FillStringParameters(bootTemplate, new BootTsModel
            {
                Code = Names.ApplyConvension(tenant, AppParts.Route),
                ModulePath = Names.ApplyConvension(tenant + "/app", AppParts.Module),
                OtherTenants = await unit.TenantRepository.Exist(e => e.Code != tenant)
            });
            string bootPath = Names.GetSrcFolderPath("main-" + Names.ApplyConvension(tenant, AppParts.Project), ".ts", keepNameformat: true);
            File.WriteAllText(bootPath, boot);
            return new Result();
        }

        private bool CheckConfigImport(string line, string configVariableName)
        {
            return line.Contains(configVariableName) && line.Contains("import");
        }

        public async Task<Result> RestructureApp(string tenantCode)
        {
            var pageCategories = await unit.PageCategoryRepository.GetList();
            var htmlService = GetService<IViewsService>();
            var domains = new Dictionary<string, PageCategoryDomainDataObject>();
            foreach (var category in pageCategories)
            {
                var oldPath = Names.GetBaseComponentFilePath(category.ViewPath);
                var newPath = oldPath.Replace("-base", "");
                var newClassName = category.Name + "Component";
                var jsonFilePath = newPath + ".config.ts";
                var domainPath = newPath.GetBeforeLast("/");
                var configVariableName = category.Name.LCFirst() + "Pages";
                try
                {

                    var template = await htmlService.GetPageCategoryById(category.Id);
                    var newHtmlPath = newPath + ".html";
                    await File.WriteAllTextAsync(newHtmlPath, template.TemplateContent);

                    var pagesJson = new Dictionary<string, PageConfigurationDto>();
                    var pages = await htmlService.GetPagesByCategory(category.Id);
                    foreach (var page in pages)
                    {
                        pagesJson[page.PageIdentifier] = page;
                    }
                    var pagesJsonString = pagesJson.ToJsonIndent();

                    var pagesJsonContent = $"const {configVariableName} = {pagesJsonString};\r\n\r\n";
                    pagesJsonContent += $"export {{ {configVariableName} }}";
                    await File.WriteAllTextAsync(jsonFilePath, pagesJsonContent);

                }
                catch (CodeShellHttpException ex)
                {
                    Out.WriteLine(ex.GetFullMessage());
                }

                if (!domains.ContainsKey(domainPath))
                {
                    domains[domainPath] = new PageCategoryDomainDataObject
                    {
                        DomainName = category.ViewPath.GetBeforeLast("/").GetAfterLast("/")
                    };
                }

                domains[domainPath].Components.Add(new PageCategoryDataObject
                {
                    Path = "./" + Names.ApplyConvension(newClassName, AppParts.Component),
                    Name = newClassName
                });

                var oldTsPath = oldPath + ".ts";

                Console.WriteLine(newPath);

                var oldClassName = category.Name + "Base";
                var content = new string[0];
                if (File.Exists(oldTsPath))
                {
                    content = File.ReadAllLines(oldTsPath);
                }
                else if (File.Exists(newPath + ".ts"))
                {
                    content = File.ReadAllLines(newPath + ".ts");
                }

                var newContent = new List<string>();

                var configImportation = $"import {{ {configVariableName} }} from \"./{newPath.GetAfterLast("/")}.config\";";
                var configFn = new string[] {
                    "\tprotected registerPageConfig(): void {",
                    $"\t\tthis.PageConfig.addPages(\"{category.Name}\", {configVariableName});",
                    "\t}"
                };

                var oldDeclaration = $"export abstract class {oldClassName}";
                var newDeclaration = $"export class {newClassName}";
                var componentData = $"@Component({{ templateUrl : './{Names.ApplyConvension(category.Name, AppParts.Component)}.html',selector : '{Names.GetComponentSelector(category.Name)}'}})";
                var ignore = false;
                var configIsAdded = false;
                var addConfigFn = false;

                foreach (var line in content)
                {
                    if (!configIsAdded)
                    {
                        if (CheckConfigImport(line, configVariableName))
                        {
                            configIsAdded = true;
                            addConfigFn = false;
                        }
                    }

                    if (line.Contains("@Component"))
                    {
                        ignore = true;
                        if (!configIsAdded)
                        {
                            newContent.Add(configImportation);
                            newContent.Add("");
                            configIsAdded = true;
                            addConfigFn = true;
                        }
                        continue;
                    }

                    if (line.Contains(oldDeclaration) || line.Contains(newDeclaration))
                    {
                        ignore = false;
                        newContent.Add(componentData);
                        if (line.Contains(oldDeclaration))
                        {
                            newContent.Add(line.Replace(oldDeclaration, newDeclaration));
                        }
                        else
                        {
                            newContent.Add(line);
                        }

                        if (addConfigFn)
                        {
                            newContent.Add("");
                            foreach (var cLine in configFn)
                                newContent.Add(cLine);
                            newContent.Add("");
                        }
                    }
                    else if (!ignore)
                    {
                        newContent.Add(line);
                    }
                }
                if (!ignore)
                    await File.WriteAllLinesAsync(newPath + ".ts", newContent);
                if (File.Exists(oldTsPath))
                    File.Delete(oldTsPath);
                //}


            }

            await _generateDomains(domains);

            return new Result();
        }


        public async Task<Result> CategoriesToComponents(string tenantCode)
        {
            var pageCategories = await unit.PageCategoryRepository.GetList();
            var htmlService = GetService<IViewsService>();
            var domains = new Dictionary<string, PageCategoryDomainDataObject>();
            foreach (var category in pageCategories)
            {
                var oldPath = Names.GetBaseComponentFilePath(category.ViewPath);
                var newPath = oldPath.Replace("-base", "").Replace("/base/", "/lib/");
                Utils.CreateFolderForFile(newPath);
                var newClassName = category.Name + "Component";
                var jsonFilePath = newPath + ".config.ts";
                var domainPath = newPath.GetBeforeLast("/");
                var configVariableName = category.Name.LCFirst() + "Pages";
                try
                {

                    var template = await htmlService.GetPageCategoryById(category.Id);
                    var newHtmlPath = newPath + ".html";
                    await File.WriteAllTextAsync(newHtmlPath, template.TemplateContent);

                    var pagesJson = new Dictionary<string, PageConfigurationDto>();
                    var pages = await htmlService.GetPagesByCategory(category.Id);
                    foreach (var page in pages)
                    {
                        pagesJson[page.PageIdentifier] = page;
                    }
                    var pagesJsonString = pagesJson.ToJsonIndent();

                    var pagesJsonContent = $"const {configVariableName} = {pagesJsonString};\r\n\r\n";
                    pagesJsonContent += $"export {{ {configVariableName} }}";
                    await File.WriteAllTextAsync(jsonFilePath, pagesJsonContent);

                }
                catch (CodeShellHttpException ex)
                {
                    Out.WriteLine(ex.GetFullMessage());
                }

                if (!domains.ContainsKey(domainPath))
                {
                    domains[domainPath] = new PageCategoryDomainDataObject
                    {
                        DomainName = category.ViewPath.GetBeforeLast("/").GetAfterLast("/")
                    };
                }

                domains[domainPath].Components.Add(new PageCategoryDataObject
                {
                    Path = "./" + Names.ApplyConvension(newClassName, AppParts.Component),
                    Name = newClassName
                });

                var oldTsPath = oldPath + ".ts";

                Console.WriteLine(newPath);

                var oldClassName = category.Name + "Base";
                var content = new string[0];
                if (File.Exists(oldTsPath))
                {
                    content = File.ReadAllLines(oldTsPath);
                }
                else if (File.Exists(newPath + ".ts"))
                {
                    content = File.ReadAllLines(newPath + ".ts");
                }

                var newContent = new List<string>();

                var configImportation = $"import {{ {configVariableName} }} from \"./{newPath.GetAfterLast("/")}.config\";";
                var configFn = new string[] {
                    "\tprotected registerPageConfig(): void {",
                    $"\t\tthis.PageConfig.addPages(\"{category.Name}\", {configVariableName});",
                    "\t}"
                };

                var oldDeclaration = $"export abstract class {oldClassName}";
                var newDeclaration = $"export class {newClassName}";
                var componentData = $"@Component({{ templateUrl : './{Names.ApplyConvension(category.Name, AppParts.Component)}.html',selector : '{Names.GetComponentSelector(category.Name)}'}})";
                var ignore = false;
                var configIsAdded = false;
                var addConfigFn = false;

                foreach (var line in content)
                {
                    if (!configIsAdded)
                    {
                        if (CheckConfigImport(line, configVariableName))
                        {
                            configIsAdded = true;
                            addConfigFn = false;
                        }
                    }

                    if (line.Contains("@Component"))
                    {
                        ignore = true;
                        if (!configIsAdded)
                        {
                            newContent.Add(configImportation);
                            newContent.Add("");
                            configIsAdded = true;
                            addConfigFn = true;
                        }
                        continue;
                    }

                    if (line.Contains(oldDeclaration) || line.Contains(newDeclaration))
                    {
                        ignore = false;
                        newContent.Add(componentData);
                        if (line.Contains(oldDeclaration))
                        {
                            newContent.Add(line.Replace(oldDeclaration, newDeclaration));
                        }
                        else
                        {
                            newContent.Add(line);
                        }

                        if (addConfigFn)
                        {
                            newContent.Add("");
                            foreach (var cLine in configFn)
                                newContent.Add(cLine);
                            newContent.Add("");
                        }
                    }
                    else if (!ignore)
                    {
                        newContent.Add(line);
                    }
                }
                if (!ignore)
                {
                    
                    await File.WriteAllLinesAsync(newPath + ".ts", newContent);
                }
                //if (File.Exists(oldTsPath))
                //    File.Delete(oldTsPath);
                //}


            }

            await _generateDomains(domains);

            return new Result();
        }

        private async Task _generateDomains(Dictionary<string, PageCategoryDomainDataObject> domains)
        {
            var mold = Molds.GetResourceByNameAsString(MoldNames.SharedModule_ts);
            foreach (var domain in domains)
            {
                var moduleName = domain.Value.DomainName + "Base";
                var path = $"{domain.Key}/{Names.ApplyConvension(moduleName, AppParts.Module)}.ts";
                if (!File.Exists(path))
                {
                    var model = new DomainTsModel
                    {
                        BaseAppModuleName = "FMSBaseModule",
                        BaseAppModulePath = "@base/fms-base.module",
                        Name = moduleName,
                        ComponentImports = "",
                        Registrations = "",
                        EmbeddedComponents = "",
                        Components = ""
                    };

                    foreach (var module in domain.Value.Components)
                    {
                        model.ComponentImports += $"import {{ {module.Name} }} from \"{module.Path}\";\n";
                    }
                    if (domain.Value.Components.Any())
                        model.Components = string.Join(", ", domain.Value.Components.Select(e => e.Name));

                    var data = Writer.FillStringParameters(mold, model);
                    //var files = Directory.GetFiles(domain.Key, "*.module.ts");
                    //foreach (var file in files)
                    //{
                    //    File.Delete(file);
                    //}

                    Utils.CreateFolderForFile(path);
                    await File.WriteAllTextAsync(path, data);
                }

            }
        }
    }
}
