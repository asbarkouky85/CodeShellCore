using CodeShellCore.Cli;
using CodeShellCore.CliDispatch.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.CliDispatch
{
    public static class CliDiExtensions
    {
        public static ICliRouteBuilder GetCliRouteBuilder(this IServiceCollection services)
        {
            var builder = services.GetSingletonInstanceOrNull<ICliRouteBuilder>();

            if (builder == null)
            {
                builder = new CliRouteBuilder(services);
                services.AddSingleton<ICliRouteBuilder>(builder);
            }
            return builder;
        }

        public static void AddConsoleControllerHander<TCont>(this ICliRouteBuilder builder) where TCont : ConsoleController
        {
            builder.AddDefaultHandler<ConsoleControllerHandler<TCont>>();
        }
    }
}
