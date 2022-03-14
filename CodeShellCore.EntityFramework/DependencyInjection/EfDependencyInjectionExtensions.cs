using CodeShellCore.Data;
using CodeShellCore.Data.Attachments;
using CodeShellCore.Data.CustomFields;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Data.Localization;
using CodeShellCore.Data.Localization.Internal;
using CodeShellCore.Localizables;
using CodeShellCore.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.DependencyInjection
{
    public static class EfDependencyInjectionExtensions
    {
        public static void AddGenericRepository(this IServiceCollection coll, Type t)
        {
            coll.AddTransient(typeof(Repository<,>), t);
            coll.AddTransient(t);
        }

        public static void AddCodeShellEntityFramework(this IServiceCollection coll)
        {
            coll.AddTransient(typeof(KeyRepository<,,>));
        }

        public static void AddAttachmentsEntity<T, TContext>(this IServiceCollection coll)
            where T : class, IAttachmentModel, IModel<long>
            where TContext : DbContext
        {
            coll.AddTransient<IAttachmentRepository<T>, DefaultAttachmentRepository<T, TContext>>();
        }

        public static void AddCustomFields<T, TContext>(this IServiceCollection coll) where T : class, ICustomField where TContext : DbContext
        {
            coll.AddTransient<ICustomFieldRepository, CustomFieldRepository<T, TContext>>();
            coll.AddTransient<CustomFieldRepository<T, TContext>>();
        }

        public static void AddDataSeeders<T>(this IServiceCollection coll, Action<DataSeederCollection<T>> seeders) where T : DbContext
        {
            var seeds = new DataSeederCollection<T>();
            seeders(seeds);
            coll.AddSingleton(typeof(DataSeederCollection<T>), seeds);
            foreach (var t in seeds.Seeders)
                coll.AddTransient(t);
        }

        public static void AddLocalizableData<TContext>(this IServiceCollection coll) where TContext : DbContext, IHasLocalizablesDbContext
        {
            coll.AddScoped<ILocalizablesUnitOfWork, LocalizablesUnitOfWork<TContext>>();
            coll.AddTransient<ILocalizationDataService, LocalizationDataService<Localizable>>();
            coll.AddTransient<ILocalizablesRepository<Localizable>, LocalizableRepository<Localizable, TContext>>();
        }

        public static void AddLocalizableData<T, TContext>(this IServiceCollection coll) where T : class, ILocalizable where TContext : DbContext, IGetLocalizedDbContext
        {
            coll.AddScoped<ILocalizablesUnitOfWork, LocalizablesUnitOfWork<TContext>>();
            coll.AddTransient<ILocalizationDataService, LocalizationDataService<T>>();
            coll.AddTransient<ILocalizablesRepository<T>, LocalizableRepository<T, TContext>>();
        }

        public static void AddCodeshellDbContext<T>(this IServiceCollection coll, bool setAsDefault = true, IConfiguration config = null, string connectionStringKey = null) where T : DbContext
        {
            if (config != null)
            {
                var conn = config.GetConnectionString("Default");
                if (connectionStringKey != null)
                    conn = config.GetConnectionString(connectionStringKey) ?? conn;
                if (string.IsNullOrEmpty(conn))
                    throw new Exception($"Connot find connection string '{connectionStringKey}' or 'Default' in appsettings");
                coll.AddDbContext<T>(e => e.UseSqlServer(conn));
            }
            else
            {
                coll.AddDbContext<T>();
            }

            if (setAsDefault)
            {
                coll.AddScoped<DbContext, T>();
                coll.AddScoped(typeof(T), d => (T)d.GetRequiredService<DbContext>());
            }
        }
    }
}
