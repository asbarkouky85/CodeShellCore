using System;
using System.Diagnostics;

namespace CodeShellCore.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Debugger.IsAttached)
            {
                args = new[] { "migrate", "-p", @"C:\_git\Asga\FMS_net5\FMS.Configuration.Api", "--tenant", "BaseLine" };
            }
            var sh = new CliShell(args);
            var t = sh.DispatchAsync();
            t.Wait();
        }
    }
}
