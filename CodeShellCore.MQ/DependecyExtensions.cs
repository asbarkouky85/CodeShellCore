using CodeShellCore.MQ.MediatR;
using CodeShellCore.MQ.RabbitMQ;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

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
            coll.AddMediatR(e =>
            {
                assemblies = assemblies ?? new[] { Shell.ProjectAssembly };
                e.RegisterServicesFromAssemblies(assemblies);
            });
            coll.AddTransient<IServiceBus, MediatRServiceBus>();
        }


    }
}
