using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.DependencyInjection
{
    public class ScopeContainer : IDisposable
    {
        public bool IsDisposed { get; private set; }
        public IServiceScope Scope { get; private set; }

        public ScopeContainer(IServiceScope scope)
        {
            IsDisposed = false;
            Scope = scope;
        }
        public void Dispose()
        {
            IsDisposed = true;
            Scope.Dispose();

        }
    }
}
