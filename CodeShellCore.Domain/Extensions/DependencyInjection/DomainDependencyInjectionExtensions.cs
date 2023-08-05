using CodeShellCore.Data.Seed;
using CodeShellCore.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CodeShellCore.Extensions.DependencyInjection
{
    public static class DomainDependencyInjectionExtensions
    {
        public static IServiceCollection AddDataSeeders<T>(
            this IServiceCollection services,
            Assembly assembly,
            Action<CodeshellDataSeedOptions> action = null) where T : class, IDataSeeder
        {
            services.AddTransient<IDataSeeder, T>();
            var seederTypes = assembly.GetTypes().Where(e => e.Implements(typeof(IDataSeedContributor)));
            services.Configure<CodeshellDataSeedOptions>(e =>
            {
                foreach (var type in seederTypes)
                {
                    e.AddDataSeedContributor(type);
                }
                action?.Invoke(e);
            });
            return services;
        }

        public static IServiceCollection AddDataSeeders(
            this IServiceCollection services,
            Assembly assembly,
            Action<CodeshellDataSeedOptions> action = null)
        {
            services.AddTransient<IDataSeeder, DataSeeder>();
            services.AddOptions<CodeshellDataSeedOptions>();
            var seederTypes = assembly.GetTypes().Where(e => e.Implements(typeof(IDataSeedContributor)));

            foreach (var t in seederTypes)
            {
                services.AddTransient(t);
            }

            services.Configure<CodeshellDataSeedOptions>(e =>
            {
                foreach (var type in seederTypes)
                {
                    e.AddDataSeedContributor(type);

                }
                action?.Invoke(e);
            });
            return services;
        }
    }
}
