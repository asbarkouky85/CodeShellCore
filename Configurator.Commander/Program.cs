using CodeShellCore.Cli;
using Configurator.Commander.Cli;
using System;

namespace Configurator.Commander
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleShell.Start<MainController>(new CommanderShell());
        }
    }
}
