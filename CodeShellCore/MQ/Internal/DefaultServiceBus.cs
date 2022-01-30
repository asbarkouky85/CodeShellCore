using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeShellCore.MQ.Internal
{
    public class DefaultServiceBus : IServiceBus, IDisposable
    {
        Dictionary<Type, List<Type>> _handlers = new Dictionary<Type, List<Type>>();

        public DefaultServiceBus(Action<ServiceBusConfig> regAction)
        {

            var _config = new ServiceBusConfig();
            regAction(_config);
            _handlers = _config.GetConsumers();
        }

        public void Dispose()
        {
            _handlers = new Dictionary<Type, List<Type>>();
        }

        public void Exit()
        {
            Dispose();
        }

        public Task PublisAsync(object ob, Type t = null, CancellationToken? token = null)
        {
            return new Task(() =>
            {
                Publish(ob, t);
            });
        }

        public void Publish(object ob, Type t = null)
        {
            if (_handlers.TryGetValue(t, out List<Type> lst))
            {
                foreach (var l in lst)
                {
                    using (var sc = Shell.GetScope())
                    {
                        var conType = typeof(ConsumptionContext<>).MakeGenericType(t);
                        var context = Activator.CreateInstance(conType, new[] { sc.ServiceProvider, ob });
                        MethodInfo meth = l.GetMethod("Consume", new[] { conType });
                        var handler = Activator.CreateInstance(l);
                        meth.Invoke(handler, new[] { context });
                    }
                }
            }
        }

        public void Start()
        {

        }
    }
}
