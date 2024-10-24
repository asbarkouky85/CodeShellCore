using CodeShellCore.Data;
using CodeShellCore.Data.Attachments;
using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Data.CustomFields;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Data.Localization;
using CodeShellCore.EntityFramework;
using CodeShellCore.EntityFramework.DesignTime;
using CodeShellCore.Localizables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.Extensions.DependencyInjection
{
    public static class EfDependencyInjectionExtensions
    {
        public static void AddGenericRepository(this IServiceCollection coll, Type t)
        {
            coll.AddTransient(typeof(Repository<,>), t);
            coll.AddTransient(t);
        }


        /// <summary>
        /// Registers classes used for configured collections that integrates with moldster
        /// </summary>
        /// <param name="coll"></param>
        /// <param name="repository">Must implement <see cref="ICollectionEFRepository{T, TContext}"/></param>
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
        /// <param name="repository">Must implement <see cref="ICollectionEFRepository{T, TContext}"/></param>
        public static void AddConfiguredCollections<TService>(this IServiceCollection coll, Type repository)
            where TService : class, ICollectionConfigService
        {
            coll.AddSingleton<TService>();
            coll.AddSingleton<ICollectionConfigService, TService>();
            coll.AddTransient(repository);
            coll.AddTransient(typeof(ICollectionEFRepository<,>), repository);
        }

        public static void AddAttachmentsEntity<T, TContext>(this IServiceCollection coll)
            where T : class, IAttachmentEntity, IEntity<long>
            where TContext : DbContext
        {
            coll.AddTransient<IAttachmentRepository<T>, DefaultAttachmentRepository<T, TContext>>();
        }

        public static void AddCustomFields<T, TContext>(this IServiceCollection coll) where T : class, ICustomField where TContext : DbContext
        {
            coll.AddTransient<ICustomFieldRepository, CustomFieldRepository<T, TContext>>();
            coll.AddTransient<CustomFieldRepository<T, TContext>>();
        }

        public static void AddLocalizableData<TContext>(this IServiceCollection coll) where TContext : CodeShellDbContext<TContext>, IHasLocalizablesDbContext
        {
            coll.AddScoped<ILocalizablesUnitOfWork, LocalizablesUnitOfWork<TContext>>();
            coll.AddTransient<ILocalizationDataService, LocalizationDataService<Localizable>>();
            coll.AddTransient<ILocalizablesRepository<Localizable>, LocalizableRepository<Localizable, TContext>>();
        }

        public static void AddLocalizableData<T, TContext>(this IServiceCollection coll) where T : class, ILocalizable where TContext : CodeShellDbContext<TContext>, IGetLocalizedDbContext
        {
            coll.AddScoped<ILocalizablesUnitOfWork, LocalizablesUnitOfWork<TContext>>();
            coll.AddTransient<ILocalizationDataService, LocalizationDataService<T>>();
            coll.AddTransient<ILocalizablesRepository<T>, LocalizableRepository<T, TContext>>();
        }

        public static void AddCodeshellDbContext<T>(this IServiceCollection coll, string migrationAssembly = null, Action<DbContextOptionsBuilder> optionsAction = null) where T : DbContext
        {
            if (migrationAssembly != null)
                DesignTimeMigrationsAssemblies.Store[typeof(T).Name] = migrationAssembly;

            coll.AddDbContext<T>(optionsAction);
        }

        public static void SetDefaultDbContext<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
        {
            services.AddScoped<DbContext, TDbContext>();
            services.AddScoped(typeof(TDbContext), d => (TDbContext)d.GetRequiredService<DbContext>());
        }

        public static void SetDefaultUnitOfWork<TUnit>(this IServiceCollection services) where TUnit : class, IUnitOfWork
        {
            services.AddScoped<IUnitOfWork, TUnit>();
        }

    }
}
