using CodeShellCore.Data;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Types;
using CodeShellCore.Text.Localization;

namespace CodeShellCore.Data.Services
{
    public class DataService<T> : ServiceBase where T : class, IUnitOfWork
    {
        protected T Unit { get; private set; }
        protected IObjectMapper Mapper { get; private set; }
        protected InstanceStore Store;
        protected ILocaleTextProvider Strings => Store.GetRequiredService<ILocaleTextProvider>();
        public DataService(T unit)
        {
            Unit = unit;
            Mapper = unit.ServiceProvider.GetService<IObjectMapper>();
            Store = new InstanceStore(() => unit.ServiceProvider);

        }
    }
}
