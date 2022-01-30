using CodeShellCore.MQ.MediatR;
using CodeShellCore.MQ.RabbitMQ;
using MassTransit.RabbitMqTransport;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CodeShellCore.MQ
{
    public static class DependecyExtensions
    {
        public static void AddRabbitMQServiceBus(this IServiceCollection coll, Action<IRabbitMqReceiveEndpointConfigurator> config, string queueName = null)
        {
            var bus = new RabbitMQServiceBus(config);
            BusConfig.Current = new BusConfig
            {
                EndPointId = queueName ?? Shell.ProjectAssembly.GetName().Name
            };
            coll.AddSingleton<IServiceBus>(bus);
        }

        public static void AddMediateRServiceBus(this IServiceCollection coll, Action<MediatRServiceConfiguration> conf, Assembly[] assemblies = null)
        {
            assemblies = assemblies ?? new[] { Shell.ProjectAssembly };
            coll.AddMediatR(conf, assemblies);
            coll.AddTransient<IServiceBus, MediatRServiceBus>();
        }


    }
}
