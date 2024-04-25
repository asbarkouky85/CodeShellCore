using CodeShellCore.DependencyInjection;
using CodeShellCore.Extensions;
using CodeShellCore.Extensions.DependencyInjection;
using CodeShellCore.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.HealthCheck
{
    public static class HealthCheckEntityFrameworkModule
    {
        public static void AddHealthCheckEntityFramework(this IServiceCollection coll, bool asDefaultModule, string? migrationAssembly = null)
        {
            coll.AddCodeshellDbContext<HealthCheckDbContext>(asDefaultModule, migrationAssembly);
            coll.AddCodeShellEntityFramework();
            if (asDefaultModule)
            {
                coll.AddUnitOfWork<HealthCheckUnit, IHealthCheckUnit>();
            }
            else
            {
                coll.AddScoped<HealthCheckUnit>();
                coll.AddScoped<IHealthCheckUnit, HealthCheckUnit>();
            }

            coll.AddDbMigrationsService<HealthCheckDbMigrationService>();

            coll.AddDataSeeders(typeof(IHealthCheckUnit).Assembly);
        }
    }
}
