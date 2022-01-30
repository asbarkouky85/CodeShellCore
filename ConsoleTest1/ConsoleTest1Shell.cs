using CodeShellCore.Cli;
using CodeShellCore.MQ;
using Asga.Auth;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using ExampleProject.Commander.Services;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Services.Email;
using CodeShellCore.Moldster;

namespace ExampleProject.Commander
{
    public class ConsoleTest1Shell : ConsoleShell
    {
        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);
            coll.AddRdlcGenerator();

            coll.AddTransient<InjectionTest>();
            coll.AddScoped<ScopedClass>();
            coll.AddTransient<EmailService>();
            coll.AddAuthModule(true);
            coll.AddRabbitMQServiceBus(d =>
            {

            });

            coll.AddMoldsterCli();
        }

        protected override void OnReady()
        {
            base.OnReady();
            //Transporter.Publish()
        }
    }
}
