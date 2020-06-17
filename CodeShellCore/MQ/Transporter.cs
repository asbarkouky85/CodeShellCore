using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.MQ
{
    public static class Transporter
    {
        private static IServiceBus Bus;

        static Transporter()
        {
            Bus = Shell.RootInjector.GetRequiredService<IServiceBus>();
        }

        public static void Publish(object ob, Type t = null)
        {
            Bus.Publish(ob, t ?? ob.GetType());
        }

        public static void Publish<T>(T ob, Type t = null)
        {
            Bus.Publish(ob, t ?? typeof(T));
        }

        public static void Start()
        {
            Bus.Start();
        }

        public static void Exit()
        {
            Bus.Exit();
        }
    }
}
