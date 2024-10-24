using CodeShellCore.Caching.Redis;
using CodeShellCore.Modularity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using static ServiceStack.Script.Lisp;

namespace CodeShellCore.Caching
{
    [DependsOn(typeof(CodeShellCoreModule))]
    public class CodeShellRedisCachingModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddTransient<ICacheProvider, RedisCacheService>();
            context.Services.AddTransient<IDbCacheProvider, RedisCacheService>();
        }
    }
}
