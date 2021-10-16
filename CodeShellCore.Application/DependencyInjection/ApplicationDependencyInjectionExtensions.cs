using CodeShellCore.Data;
using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Services;
using CodeShellCore.MQ;
using CodeShellCore.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.DependencyInjection
{
    public static class ApplicationDependencyInjectionExtensions
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

        public static void AddCodeShellApplication(this IServiceCollection coll)
        {
            coll.AddTransient(typeof(IEntityService<>), typeof(EntityService<>));
            coll.AddTransient<IUnitOfWork, DefaultUnitOfWork>();
            coll.AddScoped<IUserAccessor, UserAccessor>();
            coll.AddScoped<UserAccessor>();
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
    }
}
