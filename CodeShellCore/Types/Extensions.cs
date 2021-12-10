using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
            else if (type == typeof(string))
                return true;
            return false;
        }

        public static Type RealType(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                return type.GetGenericArguments()[0];
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

        public static string ToShortString(this Type type)
        {
            if (type == typeof(string))
                return "string";
            if (type == typeof(long))
                return "long";
            if (type == typeof(double))
                return "double";
            if (type == typeof(int))
                return "int";
            if (type == typeof(decimal))
                return "decimal";
            if (type == typeof(float))
                return "float";
            if (type == typeof(short))
                return "short";
            if (type == typeof(bool))
                return "bool";
            if (type == typeof(byte))
                return "byte";
            return type.Name;
        } 

        public static string ToPropertyTypeString(this Type type)
        {
           
            var typeString = "";
            if (type.IsGenericType)
            {
                var nullable = type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
                var genericArgs = type.GetGenericArguments();
                if (genericArgs.Length == 1 && nullable && genericArgs.First().IsValueType)
                {
                    typeString = genericArgs.First().ToShortString() + "?";
                }
                else
                {
                    typeString = type.Name + "<";
                    typeString += string.Join(',', genericArgs.Select(e => e.ToShortString()));
                    typeString += ">";
                }
            }
            else
            {
                typeString = type.ToShortString();
            }
            return typeString;
        }

        public static bool IsDouble(this Type type, bool includeNullable = false)
        {
            if (includeNullable)
                type = type.RealType();
            return type.Equals(typeof(double));
        }

        public static bool IsDecimalType(this Type type, bool includeNullable = false)
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
