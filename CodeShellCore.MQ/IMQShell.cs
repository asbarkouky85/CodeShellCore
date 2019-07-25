using System;
using System.Collections.Generic;
using System.Text;
using MassTransit.RabbitMqTransport;

namespace CodeShellCore.MQ
{
    public interface IMQShell
    {
        BusConfig BusConfig { get; }

        void RegisterConsumers(IRabbitMqReceiveEndpointConfigurator e);
    }
}
