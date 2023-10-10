using AutoMapper;
using CodeShellCore.Data;
using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Data.Services;
using CodeShellCore.MQ;
using CodeShellCore.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
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
        public static void AddCodeShellAutoMapper(this IServiceCollection collection)
        {
            
            collection.AddAutoMapper(typeof(CodeShellAutoMapperProfile).Assembly);
            collection.AddTransient<Data.Mapping.IObjectMapper, AutoMapperObjectMapper>();
            collection.AddTransient<IQueryProjector, AutoMapperObjectMapper>();
        }

        public static void AddCodeShellApplication(this IServiceCollection coll)
        {
            coll.AddTransient(typeof(IEntityService<>), typeof(EntityService<>));
            coll.AddTransient<IUnitOfWork, DefaultUnitOfWork>();


            coll.AddCodeShellAutoMapper();

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

    }
}
