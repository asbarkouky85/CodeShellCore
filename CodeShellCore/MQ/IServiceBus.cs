using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeShellCore.MQ
{
    public interface IServiceBus
    {
        void Start();
        void Exit();

        void Publish(object ob, Type t = null);
        Task PublisAsync(object ob, Type t = null, CancellationToken? token = null);
    }
}
