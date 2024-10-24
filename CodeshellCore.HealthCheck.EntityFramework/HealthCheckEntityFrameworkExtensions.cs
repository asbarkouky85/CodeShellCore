using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.HealthCheck
{
    public static class HealthCheckEntityFrameworkExtensions
    {
        public static void AddHealthCheckEntityFramework(this IServiceCollection coll, bool asDefaultModule, string? migrationAssembly = null)
        {

        }
    }
}
