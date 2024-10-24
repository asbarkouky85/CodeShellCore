using CodeShellCore.Moldster.CodeGeneration;
using CodeShellCore.Moldster.CodeGeneration.Services;
using CodeShellCore.Moldster.Domains;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CodeShellCore.Moldster
{
    public enum MoldsType { Json, Db }
    public static class Extensions
    {
        public static void UseEnumerationMapping(this IServiceCollection coll, string toPath, Func<IScriptModelMappingService, string> f)
        {
            var mapp = new ScriptMapping(f)
            {
                FilePath = toPath
            };
            ScriptMapSettings.Add(mapp);
        }

        public static void AddScriptMapping(this IServiceCollection coll, string toPath, Func<IScriptModelMappingService, string> f)
        {
            var mapp = new ScriptMapping(f)
            {
                FilePath = toPath
            };
            ScriptMapSettings.Add(mapp);
        }

        public static void AddMoldsterModules(this IServiceCollection coll, Action<MoldsterModulesConfig> modules)
        {
            var conf = new MoldsterModulesConfig();
            modules(conf);
        }
    }
}
