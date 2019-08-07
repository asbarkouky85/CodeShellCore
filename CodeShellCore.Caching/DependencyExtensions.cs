using CodeShellCore.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Caching
{
    public static class DependencyExtensions
    {
        public static void AddRedisCaching(this IServiceCollection coll)
        {
            coll.AddTransient<ICacheProvider, RedisCacheService>();
            coll.AddTransient<IDbCacheProvider, RedisCacheService>();
        }
    }
}
