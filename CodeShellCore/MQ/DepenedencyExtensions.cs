using CodeShellCore.MQ.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MQ
{
    public static class DepenedencyExtensions
    {
        public static void AddDefaultServiceBus(this IServiceCollection coll, Action<ServiceBusConfig> consumersAction)
        {
            var bus = new DefaultServiceBus(consumersAction);
            coll.AddSingleton<IServiceBus>(bus);
        }
    }
}
