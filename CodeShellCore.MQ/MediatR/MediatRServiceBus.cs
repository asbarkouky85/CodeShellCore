using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CodeShellCore.MQ.MediatR
{
    public class MediatRServiceBus : EventBusBase
    {
        private readonly IMediator mediator;

        public MediatRServiceBus(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public override Task PublisAsync(object ob, Type t = null, CancellationToken? token = null)
        {
            return mediator.Publish(ob, token ?? CancellationToken.None);
        }
    }
}
