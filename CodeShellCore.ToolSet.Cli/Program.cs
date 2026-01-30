using CodeShellCore.Extensions.Hosting;
using CodeShellCore.Files.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Reflection;

namespace CodeShellCore.ToolSet
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Debugger.IsAttached)
            {
                var testing = FunctionTypes.Http;

                switch (testing)
                {
                    case FunctionTypes.SetVersion:
                        args = new[] { "set-version", "SASO.Attachments.Domain", "1.0.0.2", @"C:\_abdelrahman\Dev\Maneh\ManehBackend\modules" };
                        break;
                    case FunctionTypes.UploadNuget:
                        args = new[] { "upload-nuget", @"C:\_abdelrahman\Personal\_gitHub\CodeShellCore", @"C:\_abdelrahman\Personal\Nuget" };
                        args = new[] { "upload-nuget", @"C:\_abdelrahman\Personal\_gitHub\CodeShellCore\CodeShellCore.ToolSet.Cli", @"ftp:genial\ftp_user/Genial963258741@196.202.126.106:21::P::/NugetServer/Packages" };
                        break;
                    case FunctionTypes.Zip:
                        args = new[] { "zip", @"C:\_abdelrahman\Work\ziptest", @"C:\_abdelrahman\Work\ziptest.zip" };
                        break;
                    case FunctionTypes.UnZip:
                        args = new[] { "extract", @"C:\_abdelrahman\Work\Common\tests\zip_*.zip", @"c:\_abdelrahman\Work\Common\tests\extracted" };
                        break;
                    case FunctionTypes.Copy:
                        //args = new[] { "copy", @"C:\_abdelrahman\Work\ziptest\Info.txt", @"ftp:genial\ftp_user/Genial963258741@196.202.126.106:21::P::/" };
                        args = new[] { "copy", @"D:\Work\Common\tests\file.txt", @"D:\Work\Common\tests\copy\file_copy.txt", "-f" };
                        break;
                    case FunctionTypes.SqlRestore:
                        args = new[] { $"sql-restore", "User Id=app;Server=.;Password=123456;", "-d", "Configurator.Config_2", "-b", "C:\\ASGA_TFS\\Libraries\\Moldster\\master\\Configurator.Config.Api\\Backups\\Configurator.Config.bak" };
                        break;
                    case FunctionTypes.SqlExec:
                        args = new[] { "sql-exec", "-c", "Server=.;User Id=app;Password=123456;Database=FMS.Configuration_v2.6", "-q", "update Resources set Name=Name" };
                        break;
                    case FunctionTypes.SyncLocAbp:
                        args = new[] { @"sync-loc-abp", @"modules\Maneh.IEC\src\Maneh.IEC.Domain.Shared\Localization\IEC" };// @"C:\_abdelrahman\Dev\Maneh\ManehBackend" };
                        break;
                    case FunctionTypes.GenerateDto:
                        args = new[] { @"gen-dto", @"C:\_git\Asga\FMS_git", "FMS.Assets.Domain", "Item", "gcul", "-o", "FMS.Assets.Application.Contracts" };// @"C:\_abdelrahman\Dev\Maneh\ManehBackend" };
                        break;
                    case FunctionTypes.ReplaceParameters:
                        args = new[] { "replace", @".\Tests\Replace\settings_file.json", "-s", ".\\Tests\\Replace\\values_file.json" };
                        //args = new[] {
                        //    "replace",
                        //    @"C:\_git\Mahmoud\Databoat-Ecommerce-Frontend\src\index.html",
                        //    "-p",
                        //    "<base href=\"/\" />",
                        //    "-d",
                        //    @"{""key"":""<base href='/ds/\' />""}",
                        //};
                        break;
                    case FunctionTypes.Help:
                        args = new[] { @"help" };
                        break;
                    case FunctionTypes.Download:
                        args = new[] { "download", "https://nodejs.org/dist/v16.16.0/node-v16.16.0-x64.msi", "./Downloads" };
                        break;
                    case FunctionTypes.Proxy:
                        args = new[] { "gen-proxy", "https://localhost:44338", "D:\\_git\\Home\\agile_careers\\agile_careers_frontend\\projects\\careers\\src" };
                        break;
                    case FunctionTypes.GenerateModuleClasses:
                        args = new[] { "gen-modules", "C:\\_git\\Asga\\WebAndBackEnd" };
                        break;
                    case FunctionTypes.Analyzer:
                        args = new[] { "analyzer", "C:\\_git\\Asga\\WebAndBackEnd\\", "-cv" };
                        break;
                    case FunctionTypes.AbpExtractKeys:
                        args = new[] {
                            "abp-extract-keys",
                            "D:\\_git\\Home\\agile_careers\\agile_careers_frontend",
                            "D:\\_git\\Home\\agile_careers\\agile_careers_backend",
                            "Careers"
                        };
                        break;
                    case FunctionTypes.Http:
                        args = new[] { "http", "GET", "https://reqres.in/app/collections/notes/records" };
                        break;
                }
            }

            Logger.Set(Assembly.GetEntryAssembly().GetName().Name, "Application");
            try
            {
                BuildHost(args).Run();
            }
            catch (Exception ex)
            {
                Logger.WriteException(ex);
            }

            if (Debugger.IsAttached)
            {
                Console.WriteLine("Operation Finished press any key to continue");
                Console.ReadLine();
            }
        }

        public static IHost BuildHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(e => e.ClearProviders())
                .UseModule<ToolSetCliModule>(args)
                .Build();
    }
}
