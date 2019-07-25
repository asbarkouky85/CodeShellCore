using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using CodeShellCore.Data;
using CodeShellCore.MQ;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Data.Services;
using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Security;
using CodeShellCore.Text.Localization;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Text.Localization.Internal;
using CodeShellCore.Data.CustomFields;

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
        public static void AddServiceFor<T, TRepo>(this IServiceCollection coll) where T : class where TRepo : class, IEntityService<T>
        {
            coll.AddTransient<TRepo>();
            coll.AddTransient<IEntityService<T>, TRepo>();
        }

        public static void AddGenericRepository(this IServiceCollection coll, Type t)
        {
            coll.AddTransient(typeof(Repository<,>), t);

        }

        public static void AddGenericCollectionRepository(this IServiceCollection coll, Type t)
        {
            coll.AddTransient(typeof(ICollectionRepository<>), t);
            coll.AddTransient(typeof(CollectionRepository<,>), t);
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

        public static void AddLocalizableData<T, TContext>(this IServiceCollection coll) where T : class, ILocalizable where TContext : DbContext
        {
            coll.AddTransient<ILocalizationDataService, LocalizationDataService<T>>();
            coll.AddTransient<ILocalizablesRepository<T>, LocalizableRepository<T, TContext>>();
    
        }

        public static void AddCustomFields<T, TContext>(this IServiceCollection coll) where T : class, ICustomField where TContext : DbContext
        {
            coll.AddTransient<ICustomFieldRepository, CustomFieldRepository<T, TContext>>();
            coll.AddTransient<CustomFieldRepository<T, TContext>>();
        }




        public static void AddCollectionUnitOfWork<T>(this IServiceCollection coll) where T : class, ICollectionUnitOfWork
        {
            coll.AddScoped<ICollectionUnitOfWork>(d => d.GetRequiredService<T>());
        }

        public static void AddUnitOfWork<T,IT>(this IServiceCollection coll) where T : class,IUnitOfWork,IT where IT : class
        {
            coll.AddScoped<T>();
            coll.AddScoped<IUnitOfWork>( d => d.GetRequiredService<T>());
            coll.AddScoped<IT>(d => d.GetRequiredService<T>());
        }

        public static void AddUnitOfWork<T>(this IServiceCollection coll) where T : class, IUnitOfWork
        {
            coll.AddScoped<IUnitOfWork, T>();
            coll.AddScoped(typeof(T), d => (T)d.GetRequiredService<IUnitOfWork>());
        }

        public static void AddContext<T>(this IServiceCollection coll) where T : DbContext
        {
            coll.AddScoped<DbContext, T>();
            coll.AddScoped(typeof(T), d => (T)d.GetRequiredService<DbContext>());
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
            acc.User = user;
        }

        public static IUser GetCurrentUser(this IServiceProvider provider)
        {
            return provider.GetService<IUserAccessor>().User;
        }

        public static void AddConfiguredCollections(this IServiceCollection coll)
        {
            coll.AddSingleton<ICollectionConfigService, CollectionConfigService>();
        }

        public static void AddConfiguredCollections<T>(this IServiceCollection coll) where T : class, ICollectionConfigService
        {
            coll.AddSingleton<T>();
            coll.AddSingleton<ICollectionConfigService, T>();
        }
    }
}
