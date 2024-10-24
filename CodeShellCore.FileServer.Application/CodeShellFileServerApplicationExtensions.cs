using CodeShellCore.FileServer.Consumers;
using CodeShellCore.FileServer.Paths;
using CodeShellCore.MQ;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.FileServer
{
    public static class CodeShellFileServerApplicationExtensions
    {

        public static void AddFileServerConsumers(this IServiceCollection coll, Action<IRabbitMqReceiveEndpointConfigurator> other = null)
        {
            coll.AddRabbitMQServiceBus(e =>
            {
                e.Consumer<TempFileConfirmedConsumer>();
                other?.Invoke(e);
            });
        }
    }
}
