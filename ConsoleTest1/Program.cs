using CodeShellCore.Cli;
using ConsoleTest1;
using System;

namespace ExampleProject.Commander
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleShell.Start<MainController>(new ConsoleTest1Shell());
        }
    }
}
