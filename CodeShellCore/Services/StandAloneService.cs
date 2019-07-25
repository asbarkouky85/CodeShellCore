using CodeShellCore.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Services
{
    public class StandAloneService : ServiceBase, IDisposable
    {
        protected IServiceScope Scope;
        protected InstanceStore<object> Store { get; private set; }

        public StandAloneService() {
            Scope = Shell.GetScope();
            Store = new InstanceStore<object>(() => Scope.ServiceProvider);
        }

        public override void Dispose()
        {
            base.Dispose();
            Store.Clear();
            Scope.Dispose();
            
        }
    }
}
