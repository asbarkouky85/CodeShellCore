using CodeShellCore;
using CodeShellCore.Modularity;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.MQ;
using CodeShellCore.Services.Email;
using ExampleProject.Commander.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace ConsoleTest1
{
    [DependsOn(
        typeof(MoldsterModule),
		typeof(CodeShellRabbitMqModule)
        )]
    public class ConsoleTest1Module : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddTransient<InjectionTest>();
            context.Services.AddScoped<ScopedClass>();
            context.Services.AddTransient<EmailService>();
            context.Services.AddRabbitMQServiceBus(d =>
            {

            });
        }
    }
}