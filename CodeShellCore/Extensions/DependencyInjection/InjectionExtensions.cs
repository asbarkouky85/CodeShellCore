using CodeShellCore.MultiTenant;
using CodeShellCore.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Extensions.DependencyInjection
{
    public static class InjectionExtensions
    {
        public static void AddMultiTenantData<T>(this IServiceCollection coll) where T : class, ITenantDataProvider
        {

            coll.AddTransient<ITenantDataProvider, T>();
        }

        public static JobConfig GetJobConfig(this IServiceCollection collection)
        {
            var conf = collection.GetSingletonInstanceOrNull<JobConfig>();
            if (conf == null)
            {
                conf = new JobConfig();
                collection.AddSingleton(conf);
            }
            return conf;
        }
    }
}
