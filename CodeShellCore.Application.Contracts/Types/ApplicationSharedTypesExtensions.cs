using CodeShellCore.Data;
using CodeShellCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Types
{
    public static class ApplicationSharedTypesExtensions
    {
        public static Type RealModelType(this Type type)
        {
            if (type.Implements(typeof(IDTO)))
            {
                var ints = type.GetInterfaces();
                var t = ints.Where(d => d.Name == "IDTO`1").FirstOrDefault();
                var gens = t.GetGenericArguments();
                return gens[0];
            }
            return type;
        }

        public static string GetEntityName(this Type type, bool fullName = false)
        {
            Type ret = type;
            if (type.Implements(typeof(IDTO)))
            {
                var ints = type.GetInterfaces();
                var t = ints.Where(d => d.Name == "IDTO`1").FirstOrDefault();
                var gens = t.GetGenericArguments();
                ret = gens[0];
            }
            else
            {
                var attribute = (EntityNameAttribute)type.GetCustomAttributes(true).FirstOrDefault(e => e.GetType().IsAssignableFrom(typeof(EntityNameAttribute)));
                if (attribute != null)
                    return attribute.EntityName;
            }
            return fullName ? ret.FullName : ret.Name;
        }
    }
}
