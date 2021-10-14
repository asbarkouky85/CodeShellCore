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
                args = new[] { "migrate", "-p", @"D:\Work\Common\code\test\configApi", "--tenant", "app" };
            }
            var sh = new CliShell(args);
            var t = sh.DispatchAsync();
            t.Wait();
        }
    }
}
