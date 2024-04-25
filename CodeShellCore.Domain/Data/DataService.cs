using CodeShellCore.Data.Mapping;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Types;
using CodeShellCore.Text.Localization;

namespace CodeShellCore.Data
{
    public class DomainService<T> : ServiceBase where T : class, IUnitOfWork
    {
        protected T Unit { get; private set; }
        protected InstanceStore Store;
        protected ILocaleTextProvider Strings => Store.GetRequiredService<ILocaleTextProvider>();
        public DomainService(T unit)
        {
            Unit = unit;
            Store = new InstanceStore(() => unit.ServiceProvider);
        }
    }
}
