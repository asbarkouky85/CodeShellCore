using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace CodeShellCore.Modularity
{
    public static class ModularityExtensions
    {
        public static IEnumerable<Type> GetDependsOnModules(this Type type)
        {
            List<Type> types = new List<Type>();
            var attrs = type.GetCustomAttributes<DependsOnAttribute>();
            foreach (var attr in attrs)
            {
                types.AddRange(attr.Modules);
                foreach (var t in attr.Modules)
                {
                    types.AddRange(t.GetDependsOnModules());
                }
            }
            return types.Distinct().ToList();
        }
    }
}
