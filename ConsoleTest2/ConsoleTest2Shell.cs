using MassTransit;
using CodeShellCore.Cli;
using CodeShellCore.MQ;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest2.Consumers;

namespace ConsoleTest2
{
    public class ConsoleTest2Shell : ConsoleShell
    {
        protected override bool useTransporter => true;
        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);
            coll.AddTransient<TestConsumer>();
            coll.AddRabbitMQServiceBus(d => {
                d.Consumer<TestConsumer>();
            });
        }

        
    }
}
