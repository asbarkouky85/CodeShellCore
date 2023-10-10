
using CodeShellCore.Data.Mapping;
using CodeShellCore.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Services
{
    public class ApplicationService : ServiceBase, IDisposable
    {
        protected IObjectMapper Mapper { get; private set; }
        protected InstanceStore<object> Store { get; private set; }

        public ApplicationService(IServiceProvider provider)
        {
            Store = new InstanceStore<object>(provider);
            Mapper = provider.GetRequiredService<IObjectMapper>();
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
