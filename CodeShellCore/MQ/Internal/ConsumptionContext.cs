using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MQ.Internal
{
    public class ConsumptionContext<T> where T :class
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public T Message { get; private set; }
        public ConsumptionContext(IServiceProvider provider, T eventData)
        {
            Message = eventData;
            ServiceProvider = provider;
        }
    }
}
