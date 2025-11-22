using CodeShellCore.Extensions.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CodeShellCore.Mcp
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        public static IHost BuildHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(e => e.ClearProviders())
                .UseModule<ToolSetMcpModule>(args)
                .Build();
    }
}