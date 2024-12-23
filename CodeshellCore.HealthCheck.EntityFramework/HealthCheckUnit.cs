using CodeShellCore.Data;
using CodeShellCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.HealthCheck
{
    public class HealthCheckUnit : UnitOfWork<HealthCheckDbContext>, IHealthCheckUnit
    {
        protected override bool UseChangeColumns => true;
        public HealthCheckUnit(IServiceProvider provider) : base(provider)
        {
        }

        public IRepository<CheckItem> CheckItemRepository => GetRepositoryFor<CheckItem>();

        public async Task CleanupOldData()
        {
            try
            {
                var date = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");
                await DbContext.Database.ExecuteSqlRawAsync($"delete from CheckItems where CreatedOn<'{date}' AND StatusCode=200");

            }
            catch
            {

            }
        }
    }
}
