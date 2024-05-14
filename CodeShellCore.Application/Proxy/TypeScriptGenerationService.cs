using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeShellCore.Types;
using CodeShellCore.Text;

namespace CodeShellCore.Proxy
{
    public class TypeScriptGenerationService : ITypeScriptGenerationService
    {
        public string MapEnum(string name, Dictionary<string, long> values)
        {
            var ret = $"export enum {name} \n";
            foreach (var v in values)
            {
                ret += $"\t{v.Key} = {v.Value} ,\n";
            }
            ret += "};\n\n";
            return ret;
        }

        public string MapEntity(SchemaItemDto schema)
        {
            string generic = "";
            if (schema.GenericArgumentCount != null)
            {
                generic = "<";
                for (int i = 0; i < schema.GenericArgumentCount; i++)
                {
                    generic += ",";
                }
                generic += ">";
            }
            string cl = $"export interface {schema.Name}{generic}{{";
            foreach (var prop in schema.Properties)
            {
                cl += $"\n\t{prop.Key.LCFirst()} {(prop.Value.Nullable ? "?: null |" : ":")} {_getTsTypeString(prop.Value)}";
            }
            cl += "\n}\r\n\n";

            return cl;
        }

        private string _getTsTypeString(PropertyDto property)
        {
            switch (property.Type)
            {
                case "reference":
                    return property.SchemaName.GetAfterFirst(".");
                case "dictionary":
                    return $"{{[key:{property.GenericArguments[0].Type}]:{_getTsTypeString(property.GenericArguments[1])}}}";
                case "array":
                    return $"{_getTsTypeString(property.GenericArguments[0])}[]";
            }
            return property.Type;
        }

        public string GetTsType(Type t)
        {
            if (t == null)
                return "any";

            if (t == typeof(string))
                return "string";

            if (t.RealType() == typeof(bool))
                return "boolean";

            if (t.RealType() == typeof(DateTime))
                return "Date";

            if (t.RealType().IsDecimalType() || t.RealType().IsIntgerType())
                return "number";

            if (typeof(IDictionary).IsAssignableFrom(t))
                return "dictionary";

            if (typeof(IEnumerable).IsAssignableFrom(t))
                return "array";

            return "reference";
        }

        public bool IsRequired(Type t)
        {
            if (t.IsGenericType)
            {
                if (t.GetGenericTypeDefinition() == typeof(Nullable<>))
                    return false;
                if (t == typeof(string))
                    return false;
                if (typeof(IEnumerable).IsAssignableFrom(t))
                    return false;
            }
            return false;
        }
    }
}
