using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Types;

namespace CodeShellCore.Services
{
    public abstract class ServiceBase : IServiceBase
    {
        protected bool isDisposed=false;
        public ServiceBase()
        {
         
        }

        public virtual void Dispose()
        {
            isDisposed = true;
        }
    }
}
