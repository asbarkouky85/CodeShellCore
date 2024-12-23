using CodeShellCore.Cli;
using CodeShellCore.CliDispatch.Routing;
using CodeShellCore.Modularity;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

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
            var hostApplicationLifeTime = context.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();

            AsyncHelper.RunSync(() => _runAsync(context));

            hostApplicationLifeTime.StopApplication();

        }

        private async Task _runAsync(CodeShellApplicationInitializationContext context)
        {
            using (var sc = context.ServiceProvider.CreateScope())
            {
                try
                {
                    var functionName = context.Arguments.Length > 0 ? context.Arguments[0] : null;
                    var routeBuilder = sc.ServiceProvider.GetRequiredService<ICliRouteBuilder>();
                    var cliRequestHandler = routeBuilder.GetHandler(functionName, sc.ServiceProvider);
                    if (cliRequestHandler == null)
                    {
                        Console.WriteLine("Unknow function : " + functionName);
                        return;
                    }
                    await cliRequestHandler.HandleAsync(context.Arguments);
                }
                catch (Exception ex)
                {
                    var writer = sc.ServiceProvider.GetRequiredService<IOutputWriter>();
                    var c = new ConsoleService(writer);
                    c.WriteException(ex);
                }

            }
        }

    }
}
