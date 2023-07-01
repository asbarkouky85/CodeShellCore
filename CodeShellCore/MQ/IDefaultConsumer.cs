using CodeShellCore.MQ.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MQ
{
    public interface IDefaultConsumer { }
    public interface IDefaultConsumer<T> : IDefaultConsumer where T : class
    {
        void Consume(ConsumptionContext<T> cont);
    }
}
