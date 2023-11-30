using CodeShellCore.Helpers;
using CodeShellCore.MQ.Events;
using CodeShellCore.Tasks;
using MassTransit;
using MassTransit.RabbitMqTransport;
using RabbitMQ.Client.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeShellCore.MQ.RabbitMQ
{
    public class RabbitMQServiceBus : EventBusBase
    {
        private readonly Action<IRabbitMqReceiveEndpointConfigurator> registerConsumers;
        IBusControl Control;

        public RabbitMQServiceBus(Action<IRabbitMqReceiveEndpointConfigurator> consumers)
        {

            this.registerConsumers = consumers;

        }

        public override Task PublisAsync(object ob, Type t = null, CancellationToken? token = null)
        {
            t = t ?? ob.GetType();
            return Control?.Publish(ob, t, token ?? CancellationToken.None);
        }

        public override void Start()
        {
            BusConfig _config = BusConfig.Current;


            Control = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(_config.Uri), h =>
                {
                    h.Username(_config.User);
                    h.Password(_config.Password);
                });

                if (_config.EndPointId != null)
                {
                    cfg.ReceiveEndpoint(host, _config.EndPointId, e => Configure(e));

                }

            });
            Control.StartAsync();

        }

        private void Configure(IRabbitMqReceiveEndpointConfigurator configurator)
        {
            registerConsumers(configurator);
        }

        public override void Exit()
        {
            if (Control != null)
                Control.Stop();
        }

        public void SendCommand(string target, object message, Type messageType = null)
        {
            messageType = messageType ?? message.GetType();
            Uri uri = new Uri(Utils.CombineUrl(BusConfig.Current.Uri, target));
            ISendEndpoint endpoint = Control.GetSendEndpoint(uri).GetTaskResult();
            endpoint.Send(message, messageType);
        }

        public Task<TR> SendRequest<T, TR>(string target, T message, int secoonds = 10) where T : class where TR : class
        {
            Uri uri = new Uri(Utils.CombineUrl(BusConfig.Current.Uri, target));
            IRequestClient<T, TR> client =
                Control.CreateRequestClient<T, TR>(uri, TimeSpan.FromSeconds(secoonds));
            return client.Request(message);
        }
    }
}
