using CodeShellCore.Modularity;
using CodeShellCore;
using CodeShellCore.Extensions.DependencyInjection;

namespace CodeShellCore.HealthCheck
{
    [DependsOn(
        typeof(CodeShellEntityFrameworkModule),
        typeof(CodeShellHealthCheckDomainModule)
        )]
    public class CodeShellHealthCheckEntityFrameworkModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddCodeshellDbContext<HealthCheckDbContext>();
            context.Services.AddUnitOfWork<HealthCheckUnit, IHealthCheckUnit>();
            context.Services.AddDbMigrationsService<HealthCheckDbMigrationService>();
            
        }
    }
}