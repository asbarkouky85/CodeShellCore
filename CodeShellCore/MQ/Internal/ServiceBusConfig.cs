using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.MQ.Internal
{
    public class ServiceBusConfig
    {
        private Dictionary<Type, List<Type>> _consumers=new Dictionary<Type, List<Type>>();
        public Dictionary<Type, List<Type>> GetConsumers()
        {
            return _consumers;
        }

        public ServiceBusConfig()
        {

        }

        public void AddConsumer<T>() where T : class, IDefaultConsumer
        {
            var ints = typeof(T).GetInterfaces();
            var types = ints.Where(d => d.GenericTypeArguments.Any() && d.GetInterfaces().Contains(typeof(IDefaultConsumer))).ToList();
            if (!types.Any())
                throw new ArgumentOutOfRangeException($"cannot find any implementations of type IDefaultConsumer<T> for Type '{typeof(T).FullName}'");

            foreach (var t in types)
            {
                var eventType = t.GetGenericArguments().FirstOrDefault();
                if (!_consumers.ContainsKey(eventType))
                    _consumers[eventType] = new List<Type>();
                _consumers[eventType].Add(typeof(T));
            }

        }

        public void Clear()
        {

        }
    }
}
