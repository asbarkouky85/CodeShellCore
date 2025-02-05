using CodeShellCore.CliDispatch.Parsing;
using CodeShellCore.Extensions.Hosting;
using CodeShellCore.Files.Logging;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Reflection;

namespace CodeShellCore.Cli
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (Debugger.IsAttached)
            {
                //args = new[] { "migrate", "-p", @"C:\_git\Asga\FMS_net5\FMS.Configuration.Api", "--tenant", "BaseLine" };
                args = new[] { "restruct", "-sd", @"C:\_git\Asga\WebAndBackEnd\FMS.Configuration.Api", "-t", "base-line", "-env", "local" };
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
        }

        public static IHost BuildHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseModule<CodeShellCoreCliModule>(args, () => CliDispatcher.UseSettingFolderFromArguments(args))
                .Build();
    }
}
