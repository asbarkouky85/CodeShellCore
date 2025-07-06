using CodeShellCore.Extensions.Hosting;
using Microsoft.Extensions.Hosting;

namespace ConsoleTest2
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildHost(args).Run();
        }

        public static IHost BuildHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseModule<ConsoleTest2Module>(args)
                .Build();
    }
}
