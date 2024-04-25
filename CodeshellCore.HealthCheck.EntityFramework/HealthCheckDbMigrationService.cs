using CodeShellCore.EntityFramework;
using CodeShellCore.EntityFramework.Migrations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.HealthCheck
{
    public class HealthCheckDbMigrationService : EfDbMigrationService<HealthCheckDbContext>
    {
        public HealthCheckDbMigrationService(HealthCheckDbContext context) : base(context)
        {
        }
    }
}
