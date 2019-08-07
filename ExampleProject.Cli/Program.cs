using CodeShellCore;
using CodeShellCore.Cli;
using ExampleProject.Cli.Controllers;
using System;

namespace ExampleProject.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleShell.Start<MainController>(new CliShell());
        }
    }
}
