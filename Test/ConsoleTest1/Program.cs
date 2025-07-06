using CodeShellCore.Extensions.Hosting;
using ConsoleTest1;
using Microsoft.Extensions.Hosting;

namespace ConsoleTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildHost(args).Run();
        }

        public static IHost BuildHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseModule<ConsoleTest1Module>(args)
                .Build();
    }
}