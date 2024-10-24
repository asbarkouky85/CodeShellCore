using CodeShellCore.CliDispatch.Routing;
using CodeShellCore.Modularity;
using CodeShellCore.Security.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.CliDispatch
{
    [DependsOn(
        typeof(CodeShellApplicationModule)
        )]
    public class CodeShellCliDispatchModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddOptions<CliDispatchOptions>();
            context.Services.AddSingleton<AuthorizationService>();
        }

        public override void OnApplicationStarted(CodeShellApplicationInitializationContext context)
        {
            using (var sc = context.ServiceProvider.CreateScope())
            {
                var functionName = context.Arguments.Length > 0 ? context.Arguments[0] : null;
                var routeBuilder = sc.ServiceProvider.GetRequiredService<ICliRouteBuilder>();
                var cliRequestHandler = routeBuilder.GetHandler(functionName, sc.ServiceProvider);
                if (cliRequestHandler == null)
                {
                    Console.WriteLine("Unknow function : " + functionName);
                    return;
                }
                var task = cliRequestHandler.HandleAsync(context.Arguments);
                task.Wait();
            }
        }

    }
}
