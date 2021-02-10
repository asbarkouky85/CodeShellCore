using CodeShellCore.Cli;
using ConsoleTest2.Controllers;
using System;

namespace ConsoleTest2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var s = new ConsoleTest2Shell())
            {
                ConsoleShell.Start<MainController>(s);
            }

        }
    }
}
