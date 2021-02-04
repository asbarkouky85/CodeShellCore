using System;

using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.Data;
using CodeShellCore.MQ;
using CodeShellCore.Data.Services;
using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Security;
using CodeShellCore.Tasks;
using System.Collections.Generic;
using CodeShellCore.Files.Reporting;
using CodeShellCore.MultiTenant;
using CodeShellCore.Helpers;

namespace CodeShellCore.DependencyInjection
{
    public static class InjectionExtensions
    {
        public static void AddHandlerFor<T, TRepo>(this IServiceCollection coll) where T : class where TRepo : class, IEntityHandler<T>
        {
            coll.AddTransient<TRepo>();
            coll.AddTransient<IEntityService<T>, TRepo>();
            coll.AddTransient<IEntityHandler<T>, TRepo>();
        }
        public static void AddServiceFor<T, TService>(this IServiceCollection coll) where T : class where TService : class, IEntityService<T>
        {
            coll.AddTransient<TService>();
            coll.AddTransient<IEntityService<T>, TService>();
        }

        public static void AddRdlcGenerator(this IServiceCollection coll)
        {
            coll.AddTransient<RdlcDataSetGenerator>();
        }

        public static void AddServiceFor<T, TService, ITService>(this IServiceCollection coll)
            where T : class
            where TService : class, ITService
            where ITService : class, IEntityService<T>
        {
            coll.AddTransient<TService>();
            coll.AddTransient<IEntityService<T>, TService>();
            coll.AddTransient<ITService, TService>();
        }

        public static void AddLookupsService<T>(this IServiceCollection coll) where T : class, ILookupsService
        {
            coll.AddTransient<ILookupsService, T>();
        }

        public static void AddLookupsService<T, IT>(this IServiceCollection coll)
            where IT : class, ILookupsService
            where T : class, IT
        {
            coll.AddTransient<ILookupsService, T>();
            coll.AddTransient<IT, T>();
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

        

        

        public static T GetCurrentUserAs<T>(this IServiceProvider prov) where T : class, IUser
        {
            var user = prov.GetCurrentUser();
            if (user == null)
                return null;
            return (T)user;
        }

        public static void AddUnitOfWork<T, IT>(this IServiceCollection coll) where T : class, IUnitOfWork, IT where IT : class
        {
            coll.AddScoped<T>();
            coll.AddScoped<IUnitOfWork>(d => d.GetRequiredService<T>());
            coll.AddScoped<IT>(d => d.GetRequiredService<T>());
        }

        public static void AddUnitOfWork<T>(this IServiceCollection coll) where T : class, IUnitOfWork
        {
            coll.AddScoped<IUnitOfWork, T>();
            coll.AddScoped(typeof(T), d => (T)d.GetRequiredService<IUnitOfWork>());
        }

        public static bool TryGetService<T>(this IServiceProvider provider, out T service)
        {
            T ser = provider.GetService<T>();
            if (ser != null)
            {
                service = ser;
                return true;
            }

            service = Activator.CreateInstance<T>();
            return false;
        }

        public static void SetCurrentUser(this IServiceProvider provider, IUser user)
        {
            var acc = provider.GetService<IUserAccessor>();
            acc.Set(user);
        }

        public static void SetCurrentUserId(this IServiceProvider provider, string id, bool asClient = false)
        {
            var acc = provider.GetService<IUserAccessor>();
            acc.UserId = id;
            if (asClient)
            {
                acc.ClientId = id;
                provider.GetService<ClientData>().ClientId = id;
            }
                
        }

        public static IUser GetCurrentUser(this IServiceProvider provider)
        {
            return provider.GetService<IUserAccessor>().User;
        }

        /// <summary>
        /// Registers classes used for configured collections that integrates with moldster
        /// </summary>
        /// <param name="coll"></param>
        /// <param name="repository">Must implement <see cref="CodeShellCore.Data.ConfiguredCollections.ICollectionEFRepository{T, TContext}"/></param>
        public static void AddConfiguredCollections(this IServiceCollection coll, Type repository)
        {
            coll.AddSingleton<ICollectionConfigService, CollectionConfigService>();
            coll.AddTransient(typeof(ICollectionEFRepository<,>), repository);
        }

        /// <summary>
        /// Registers classes used for configured collections that integrates with moldster while specifying a different collection service
        /// </summary>
        /// <typeparam name="TUnit"></typeparam>
        /// <typeparam name="TService"></typeparam>
        /// <param name="coll"></param>
        /// <param name="repository">Must implement <see cref="CodeShellCore.Data.ConfiguredCollections.ICollectionEFRepository{T, TContext}"/></param>
        public static void AddConfiguredCollections<TService>(this IServiceCollection coll, Type repository)
            where TService : class, ICollectionConfigService
        {
            coll.AddSingleton<TService>();
            coll.AddSingleton<ICollectionConfigService, TService>();
            coll.AddTransient(repository);
            coll.AddTransient(typeof(ICollectionEFRepository<,>), repository);
        }

        public static void AddTimedJobs(this IServiceCollection coll, IEnumerable<ITimedJob> jobs)
        {
            JobConfig conf = new JobConfig(jobs);
            coll.AddTransient<IJobRunner, JobRunner>();
            coll.AddSingleton(conf);
        }

        public static void AddTimedJobs<T>(this IServiceCollection coll, IEnumerable<ITimedJob> jobs) where T : class, IJobRunner
        {
            JobConfig conf = new JobConfig(jobs);
            coll.AddTransient<IJobRunner, T>();
            coll.AddSingleton(conf);
        }

        public static void AddMultiTenantData<T>(this IServiceCollection coll) where T : class, ITenantDataProvider
        {
            coll.AddScoped<CurrentTenant>();
            coll.AddTransient<ITenantDataProvider, T>();
        }
    }
}
