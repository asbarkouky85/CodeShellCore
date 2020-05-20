using CodeShellCore.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Services
{
    public class StandAloneService : ServiceBase, IDisposable
    {

        protected InstanceStore<object> Store { get; private set; }

        public StandAloneService(IServiceProvider provider)
        {
            Store = new InstanceStore<object>(provider);
        }

        protected T GetService<T>()
        {
            return Store.GetInstance<T>();
        }

        public override void Dispose()
        {
            base.Dispose();
            Store.Clear();
        }
    }
}
