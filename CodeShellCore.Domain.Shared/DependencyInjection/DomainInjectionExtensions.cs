using CodeShellCore.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.DependencyInjection
{
    public static class DomainInjectionExtensions
    {
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
