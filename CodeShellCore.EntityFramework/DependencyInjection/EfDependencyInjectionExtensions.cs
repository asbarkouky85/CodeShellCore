using CodeShellCore.Data;
using CodeShellCore.Data.Attachments;
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

        public static void AddCodeshellDbContext<T>(this IServiceCollection coll, string key, IConfiguration configuration = null, bool asDefault = false, string migrationAssembly = null)
            where T : DbContext
        {
            if (configuration != null)
                coll.AddCodeshellDbContext<T>(configuration, key, asDefault, migrationAssembly);
            else
                coll.AddCodeshellDbContext<T>(asDefault);
        }

        public static void AddCodeshellDbContext<T>(this IServiceCollection coll, IConfiguration config, string connectionStringKey, bool setAsDefault = false, string migrationAssembly = null) where T : DbContext
        {
            if (migrationAssembly != null)
                DesignTimeMigrationsAssemblies.Store[typeof(T).Name] = migrationAssembly;

            var conn = _getConnectionStringOrDefault(config, connectionStringKey);
            if (conn != null)
                coll.AddDbContext<T>(e => e.UseSqlServer(conn));
            else
                coll.AddDbContext<T>();

            if (setAsDefault)
            {
                coll.AddScoped<DbContext, T>();
                coll.AddScoped(typeof(T), d => (T)d.GetRequiredService<DbContext>());
            }
        }

        private static string _getConnectionStringOrDefault(IConfiguration config, string connectionStringKey)
        {
            var conn = config.GetConnectionString("Default");
            if (connectionStringKey != null)
                conn = config.GetConnectionString(connectionStringKey) ?? conn;
            return conn;
        }

        public static void AddCodeshellDbContext<T>(this IServiceCollection coll, bool setAsDefault = true, IConfiguration config = null, string connectionStringKey = null) where T : DbContext
        {
            if (config != null)
            {
                var conn = _getConnectionStringOrDefault(config, connectionStringKey);
                if (conn != null)
                    coll.AddDbContext<T>(e => e.UseSqlServer(conn));
                else
                    coll.AddDbContext<T>();
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
