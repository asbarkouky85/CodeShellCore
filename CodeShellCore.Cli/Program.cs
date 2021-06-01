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
                args = new[] { "migrate", "-p", @"C:\_abdelrahman\Personal\_gitHub\CodeShellCore\Example.Config.Api" };
            }
            var sh = new CliShell(args);
            var t = sh.DispatchAsync();
            t.Wait();
        }
    }
}
