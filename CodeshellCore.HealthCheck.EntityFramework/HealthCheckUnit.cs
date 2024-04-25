using CodeShellCore.Data;
using CodeShellCore.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.HealthCheck
{
    public class HealthCheckUnit : UnitOfWork<HealthCheckDbContext>, IHealthCheckUnit
    {
        protected override bool UseChangeColumns => true;
        public HealthCheckUnit(IServiceProvider provider) : base(provider)
        {
        }

        public IRepository<CheckItem> CheckItemRepository => GetRepositoryFor<CheckItem>();
    }
}
