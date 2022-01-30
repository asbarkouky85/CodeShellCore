using CodeShellCore.Data.Helpers;
using CodeShellCore.MQ.Events;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTest2.Consumers
{
    public class TestConsumer : CodeShellCore.MQ.Consumer, IConsumer<SimpleEvent>, INotificationHandler<SimpleEvent>, IConsumer<SimpleEvent2>
    {
        public Task Consume(ConsumeContext<SimpleEvent> context)
        {
            return ConsumeEvent(context, d =>
            {
                Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Consumed [{d.Id}] : {d.Message}");
                return new SubmitResult(0);
            });
        }

        public Task Consume(ConsumeContext<SimpleEvent2> context)
        {
            return ConsumeEvent(context, d =>
            {
                Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Consumed [{d.Id}] : {d.Message}");
                return new SubmitResult(1);
            });
        }

        public Task Handle(SimpleEvent notification, CancellationToken cancellationToken)
        {
            return ConsumeEvent(notification, d =>
            {
                Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}] Consumed [{d.Id}] : {d.Message}");
                return new SubmitResult();
            });

        }
    }
}
