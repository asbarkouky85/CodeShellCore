using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Linq;

namespace CodeShellCore.Types
{
    public class InstanceStore<T> : Dictionary<Type, T>
    {
        IServiceProvider _injector;
        IServiceProvider Injector
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

        public T GetInstance(Type t, Action<T> onCreate = null)
        {
            if (!t.GetInterfaces().Contains(typeof(T)))
                throw new Exception(t.Name + " does not implement IRepository");
            if (!TryGetValue(t, out T service))
            {
                service = (T)Injector.GetService(t);

                if (service == null)
                    throw new Exception("Type " + t.FullName + " is not registered");
                else
                    onCreate?.Invoke(service);
                this[t] = service;
            }
            return (T)service;
        }

        public TService GetInstance<TService>(Action<T> onCreate = null) where TService : T
        {
            T service;
            Type t = typeof(TService);
            if (!TryGetValue(t, out service))
            {
                service = Injector.GetService<TService>();
                if (service == null)
                    throw new Exception("Type " + t.FullName + " is not registered");
                else
                    onCreate?.Invoke(service);
                this[t] = service;
            }
            return (TService)service;
        }


    }
}
