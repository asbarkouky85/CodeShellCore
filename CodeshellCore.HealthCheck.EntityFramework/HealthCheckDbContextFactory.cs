using CodeShellCore.EntityFramework.DesignTime;

namespace CodeShellCore.HealthCheck
{
    public class HealthCheckDbContextFactory : CodeShellDesignTimeDbContextFactory<HealthCheckDbContext>
    {
        protected override string ConnectionStringKey => "HealthChecker";

        public HealthCheckDbContextFactory()
        {

        }
    }
}
