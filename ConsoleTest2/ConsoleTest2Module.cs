using CodeShellCore.Modularity;
using CodeShellCore;
using CodeShellCore.MQ;
using ConsoleTest2.Consumers;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;

namespace ConsoleTest2
{
    [DependsOn(
        typeof(CodeShellApplicationModule),
        typeof(CodeShellRabbitMqModule)
        )]
    public class ConsoleTest2Module : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddTransient<TestConsumer>();
            context.Services.AddRabbitMQServiceBus(d =>
            {
                d.Consumer<TestConsumer>();
            });
        }
    }
}