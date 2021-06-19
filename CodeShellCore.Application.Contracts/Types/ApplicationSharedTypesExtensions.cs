using CodeShellCore.Data;
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
    }
}
