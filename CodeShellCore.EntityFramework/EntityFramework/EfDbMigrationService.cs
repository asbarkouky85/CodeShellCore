using CodeShellCore.EntityFramework.Migrations;
using CodeShellCore.MultiTenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.EntityFramework
{
    public class EfDbMigrationService<TDbCotnext> : IDbMigrationService where TDbCotnext : DbContext
    {
        private readonly TDbCotnext context;

        public EfDbMigrationService(TDbCotnext context)
        {
            this.context = context;
        }

        public async Task MigrateAsync()
        {
            await context.Database.MigrateAsync();
        }

    }
}
