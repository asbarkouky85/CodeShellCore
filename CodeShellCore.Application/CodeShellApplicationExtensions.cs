using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Data.Services;
using CodeShellCore.Files.Uploads;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore
{
    public static class CodeShellApplicationExtensions
    {

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
