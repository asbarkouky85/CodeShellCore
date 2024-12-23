using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Moldster.Domains;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.Moldster
{
    public static class Extensions
    {
        public static void AddMoldsterModules(this IServiceCollection coll, Action<MoldsterModulesConfig> modules)
        {
            var conf = new MoldsterModulesConfig();
            modules(conf);
        }
    }
}
