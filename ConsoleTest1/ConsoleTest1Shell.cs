using CodeShellCore.Cli;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Moldster;
using CodeShellCore.MQ;
using CodeShellCore.Services.Email;
using ExampleProject.Commander.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleProject.Commander
{
    public class ConsoleTest1Shell : ConsoleShell
    {
        protected override bool useTransporter => true;
        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);
            coll.AddRdlcGenerator();

            coll.AddTransient<InjectionTest>();
            coll.AddScoped<ScopedClass>();
            coll.AddTransient<EmailService>();
            coll.AddRabbitMQServiceBus(d =>
            {

            });
            coll.AddMoldsterDbData(Configuration);
            coll.AddMoldsterCli();
        }

        protected override void OnReady()
        {
            base.OnReady();
            //Transporter.Publish()
        }
    }
}
