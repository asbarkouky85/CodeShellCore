using CodeShellCore.Data;
using CodeShellCore.Data.Attachments;
using CodeShellCore.Data.CustomFields;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Data.Localization;
using CodeShellCore.Data.Localization.Internal;
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

        public static void AddLocalizableData<T, TContext>(this IServiceCollection coll) where T : class, ILocalizable where TContext : DbContext
        {
            coll.AddTransient<ILocalizationDataService, LocalizationDataService<T>>();
            coll.AddTransient<ILocalizablesRepository<T>, LocalizableRepository<T, TContext>>();
        }

        public static void AddContext<T>(this IServiceCollection coll) where T : DbContext
        {
            coll.AddScoped<DbContext, T>();
            coll.AddScoped(typeof(T), d => (T)d.GetRequiredService<DbContext>());
        }
    }
}
