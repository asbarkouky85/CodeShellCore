using CodeShellCore.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Types
{
    public static class Extensions
    {
        public static string GetVersionString(this Assembly assembly)
        {
            var asem = assembly.CustomAttributes.Where(d => d.AttributeType == typeof(AssemblyFileVersionAttribute)).FirstOrDefault();
            if (asem == null)
                return null;
            if (asem.ConstructorArguments.Count == 0)
                return null;
            return (string)asem.ConstructorArguments.FirstOrDefault().Value;
        }
        public static bool IsNullable(this Type type)
        {
            if (type.IsGenericType)
                return type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
            return false;
        }

        public static Type RealType(this Type type)
        {
            if (type.IsNullable())
                return type.GetGenericArguments()[0];
            return type;
        }

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

        public static IEnumerable<PropertyInfo> GetValueProperties(this Type type, bool ignoreId = true, string[] ignore = null)
        {
            ignore = ignore ?? new string[0];

            return type.GetProperties()
                .Where(
                    d => (d.Name != "Id" || !ignoreId) &&
                    (d.PropertyType == typeof(string) || !typeof(IEnumerable).IsAssignableFrom(d.PropertyType)) &&
                    !ignore.Contains(d.Name) &&
                    d.CanWrite
                );
        }

        public static Type InnerType(this Type type)
        {
            Type[] args = type.GetGenericArguments();
            if (args.Length > 0)
                return args[0];
            return type;
        }

        public static bool Implements(this Type type, Type check)
        {
            return type.GetInterfaces().Contains(check);
        }

        public static bool IsDouble(this Type type, bool includeNullable = false)
        {
            if (includeNullable)
                type = type.RealType();
            return type.Equals(typeof(double));
        }

        public static bool IsDecimalType(this Type type,bool includeNullable=false)
        {
            if (includeNullable)
                type = type.RealType();
            return type.Equals(typeof(decimal)) || type.Equals(typeof(double)) || type.Equals(typeof(float));
        }

        public static bool IsIntgerType(this Type type, bool includeNullable = false)
        {
            if (includeNullable)
                type = type.RealType();
            return type.Equals(typeof(sbyte)) || type.Equals(typeof(byte)) || type.Equals(typeof(int)) || type.Equals(typeof(long)) || type.Equals(typeof(uint)) || type.Equals(typeof(ulong));
        }
    }
}
