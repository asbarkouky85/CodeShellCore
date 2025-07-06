using CodeShellCore.Cli;
using CodeShellCore.CliDispatch.Routing;
using CodeShellCore.Modularity;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Threading;
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
                bool isSuccess = false;
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
                    var cts = new CancellationTokenSource();

                    if (cliRequestHandler.RunInBackground)
                    {
                        var t = cliRequestHandler.HandleAsync(context.Arguments, cts.Token);

                        t.GetAwaiter().OnCompleted(() =>
                        {
                            isSuccess = true;
                            cts.Cancel();
                        });

                        WaitForKey(cts.Token);

                        cts.Cancel();
                        cts.Token.ThrowIfCancellationRequested();
                    }
                    else
                    {
                        await cliRequestHandler.HandleAsync(context.Arguments, cts.Token);
                    }

                }
                catch (OperationCanceledException)
                {
                    if (!isSuccess)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Task Cancelled");
                    }
                }
                catch (Exception ex)
                {
                    var writer = sc.ServiceProvider.GetRequiredService<IOutputWriter>();
                    var c = new ConsoleService(writer);
                    c.WriteException(ex);
                }

            }
        }


        private ConsoleKeyInfo WaitForKey(CancellationToken token)
        {
            int delay = 0;
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    return Console.ReadKey();
                }
                token.ThrowIfCancellationRequested();
                Thread.Sleep(50);
                delay += 50;
            }
        }
    }
}
