using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Linq;
using Inflector;

namespace CodeShellCore.Types
{
    public class InstanceStore
    {
        protected Dictionary<Type, object> Registry = new Dictionary<Type, object>();
        IServiceProvider _injector;
        protected IServiceProvider Injector
        {
            get
            {
                if (ProviderDelegate != null)
                    return ProviderDelegate.Invoke();

                return _injector;
            }
        }
        Func<IServiceProvider> ProviderDelegate;

        public InstanceStore(IServiceProvider provider)
        {
            _injector = provider;
        }

        public InstanceStore(Func<IServiceProvider> provider)
        {
            ProviderDelegate = provider;
        }

        public TService GetRequiredService<TService>(Action<TService> onCreate = null)
        {

            Type t = typeof(TService);
            if (!Registry.TryGetValue(t, out object serv))
            {
                serv = Injector.GetService<TService>();
                if (serv == null)
                    throw new Exception("Type " + t.FullName + " is not registered");
                else
                    onCreate?.Invoke((TService)serv);
                Registry[t] = serv;
            }
            return (TService)serv;
        }

        public TService GetService<TService>(Action<TService> onCreate = null)
        {
            Type t = typeof(TService);
            if (!Registry.TryGetValue(t, out object serv))
            {
                serv = Injector.GetService<TService>();
                if (serv != null)
                    onCreate?.Invoke((TService)serv);
                Registry[t] = serv;
            }
            return (TService)serv;
        }

        public void Clear()
        {
            Registry.Clear();
        }
    }
    public class InstanceStore<T> : InstanceStore
    {
        public InstanceStore(IServiceProvider provider) : base(provider)
        {
        }

        public InstanceStore(Func<IServiceProvider> provider) : base(provider)
        {

        }

        public T GetInstance(Type t, Action<T> onCreate = null)
        {
            if (!t.GetInterfaces().Contains(typeof(T)))
                throw new Exception(t.Name + " does not implement IRepository");
            if (!Registry.TryGetValue(t, out object service))
            {
                service = (T)Injector.GetService(t);

                if (service == null)
                    throw new Exception("Type " + t.FullName + " is not registered");
                else
                    onCreate?.Invoke((T)service);
                Registry[t] = service;
            }
            return (T)service;
        }

        public TService GetInstance<TService>(Action<T> onCreate = null) where TService : class, T
        {
            object service;
            Type t = typeof(TService);
            if (!Registry.TryGetValue(t, out service))
            {
                service = Injector.GetService<TService>();
                if (service == null)
                    throw new Exception("Type " + t.FullName + " is not registered");
                else
                    onCreate?.Invoke((T)service);
                Registry[t] = service;
            }
            return (TService)service;
        }


    }
}
