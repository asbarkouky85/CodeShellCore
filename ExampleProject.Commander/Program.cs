using CodeShellCore.Cli;
using ExampleProject.Commander.Cli;
using System;

namespace ExampleProject.Commander
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleShell.Start<MainController>(new CommanderShell());
        }
    }
}
