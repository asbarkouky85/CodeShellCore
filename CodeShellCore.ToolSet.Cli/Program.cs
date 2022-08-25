using CodeShellCore.Cli;
using System;
using System.Diagnostics;

namespace CodeShellCore.ToolSet
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                if (Debugger.IsAttached)
                {
                    var testing = FunctionTypes.Help;

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
                            args = new[] { "replace", @"D:\Work\Common\tests\replace_test.json", "{\"userName\":\"abarkouky\",\"password\":\"123456\"}" };
                            break;
                        case FunctionTypes.Help:
                            args = new[] { @"help" };
                            break;

                    }
                }

                var sh = new ToolSetShell(args);
                var t = sh.DispatchAsync();
                t.Wait();
                if (Debugger.IsAttached)
                {
                    using (ColorSetter.Set(ConsoleColor.Green))
                    {
                        Console.WriteLine();
                        Console.WriteLine(" Request Completed");
                        Console.WriteLine();
                    }
                }


            }
            catch (Exception ex)
            {
                using (ColorSetter.Set(ConsoleColor.DarkRed))
                {
                    Console.WriteLine(" Request Failed");
                    Console.WriteLine(ex.GetMessageRecursive());
                    var stackRec = ex.GetStackTrace(true);
                    foreach (var l in stackRec)
                    {
                        Console.WriteLine(l);
                    }

                }
            }

            if (Debugger.IsAttached)
            {
                Console.ReadLine();
            }
        }
    }
}
