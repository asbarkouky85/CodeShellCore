using CodeShellCore.Helpers;
using CodeShellCore.Tasks;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.MQ
{
    public class Transporter
    {
        private static IBusControl Control;
        private static BusConfig _config;
        public static void Run(Action<IBusControl> func)
        {
            if (Control != null)
                func.Invoke(Control);
        }

        public static void Publish(object ob, Type t = null)
        {
            t = t ?? ob.GetType();
            Task tsk = Control?.Publish(ob, t);
            if (tsk != null)
                Task.WaitAll(tsk);
        }

        public static void Publish<T>(T ob, Type t = null)
        {
            t = t ?? ob.GetType();
            Task tsk = Control?.Publish(ob, t);
            if (tsk != null)
                Task.WaitAll(tsk);
        }

        public static void SendCommand(string target, object message, Type messageType = null)
        {
            messageType = messageType ?? message.GetType();
            Uri uri = new Uri(Utils.CombineUrl(_config.Uri, target));
            ISendEndpoint endpoint = Control.GetSendEndpoint(uri).GetTaskResult();
            endpoint.Send(message, messageType);
        }

        public static Task<TR> SendRequest<T,TR>(string target , T message, int secoonds = 10) where T :class where TR : class 
        {
            Uri uri = new Uri(Utils.CombineUrl(_config.Uri, target));
            IRequestClient<T, TR> client =
                Control.CreateRequestClient<T, TR>(uri, TimeSpan.FromSeconds(secoonds));
            return client.Request(message);
        }

        public static void Start( IMQShell shell)
        {
            _config = shell.BusConfig;

            Control = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(_config.Uri), h =>
                {
                    h.Username(_config.User);
                    h.Password(_config.Password);
                });

                if (_config.EndPointId != null)
                {
                    cfg.ReceiveEndpoint(host, _config.EndPointId, e =>
                    {
                        shell.RegisterConsumers(e);
                    });
                }
            });
            Control.StartAsync();

        }

        public static void Exit()
        {
            if (Control != null)
                Control.Stop();
        }
    }
}
