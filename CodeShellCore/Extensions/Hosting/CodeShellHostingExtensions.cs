using CodeShellCore.Cli;
using CodeShellCore.Modularity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace CodeShellCore.Extensions.Hosting
{
    public static class CodeShellHostingExtensions
    {
        public static IHostBuilder UseModule<T>(this IHostBuilder builder, string[] args, Func<IConfigurationRoot> configBuilder = null) where T : CodeShellModule
        {
            var appBuilder = new CodeShellAppBuilder(args);
            appBuilder.UseModule<T>(configBuilder);

            builder.UseServiceProviderFactory(new CodeShellServiceProviderFactory(appBuilder));
            return builder;
        }
    }
}
