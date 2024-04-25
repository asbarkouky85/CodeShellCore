using CodeShellCore.Data;
using CodeShellCore.Data.Seed;
using CodeShellCore.MultiTenant;
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

        public static void AddRepositoryFor<T, TRepo, ITRepo>(this IServiceCollection coll)
            where T : class
            where ITRepo : class, IRepository<T>
            where TRepo : class, ITRepo

        {
            coll.AddTransient<TRepo>();
            coll.AddTransient<IRepository<T>, TRepo>();
            coll.AddTransient<ITRepo, TRepo>();
        }

        public static void AddRepositoryFor<T, TRepo>(this IServiceCollection coll) where T : class where TRepo : class, IRepository<T>
        {
            coll.AddTransient<TRepo>();
            coll.AddTransient<IRepository<T>, TRepo>();
        }
        public static void AddUnitOfWork<T, IT>(this IServiceCollection coll, bool setAsDefault = true) where T : class, IUnitOfWork, IT where IT : class
        {
            coll.AddScoped<T>();
            coll.AddScoped<IT>(d => d.GetRequiredService<T>());

            if (setAsDefault)
                coll.AddScoped<IUnitOfWork>(d => d.GetRequiredService<T>());
        }

        public static void AddUnitOfWork<T>(this IServiceCollection coll, bool setAsDefault = true) where T : class, IUnitOfWork
        {
            coll.AddScoped<T>();
            if (setAsDefault)
                coll.AddScoped<IUnitOfWork>(e => e.GetRequiredService<T>());
        }

    }
}
