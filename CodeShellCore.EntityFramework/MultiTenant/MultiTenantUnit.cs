using CodeShellCore.Data.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MultiTenant
{
    public class MultiTenantUnit<TContext> : UnitOfWork<TContext> where TContext : MultiTenantContext
    {
        private readonly CurrentTenant _data;
        public CurrentTenant CurrentTenant { get { return _data; } }
        public MultiTenantUnit(IServiceProvider provider) : base(provider)
        {
            _data = _provider.GetService<CurrentTenant>();
            DbContext.ConnectionString = _data.GetConnectionString();
        }
    }
}
