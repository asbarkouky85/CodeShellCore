using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeShellCore.Types;
using CodeShellCore.Text;
using CodeShellCore.Text.Localization;
using CodeShellCore.Helpers;

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

        public string GetFolderPathFromNamespace(string name_space)
        {
            var foder = _angular_convesion(name_space.Replace(".", "/"));
            foder = foder.Replace("code-shell-core", "codeshell");
            return Utils.CombineUrl("proxy", foder);
        }

        string _angular_convesion(string path)
        {
            path = path.Replace("\\", "/");
            string[] parts = path.Split("/").Select(e => LangUtils.CamelCaseToWords(e, "-").ToLower()).ToArray();
            return string.Join("/", parts);
        }

        public string MapEntity(SchemaItemDto schema)
        {
            List<PropertyDto> importations = new List<PropertyDto>();
            string generic = "";
            string className = schema.Name;
            if (schema.GenericArguments != null)
            {
                className = className.GetBeforeFirst("`");
                generic = "<";
                var sep = "";
                foreach (var arg in schema.GenericArguments)
                {
                    generic += sep + $"{arg}";
                    sep = ",";
                }
                generic += ">";
            }
            string cl = $"export interface {className}{generic}{{";
            foreach (var prop in schema.Properties)
            {
                cl += $"\n\t{prop.Key.LCFirst()}{(prop.Value.Nullable ? "?: " : ": ")} {_getTsTypeString(prop.Value)};";
            }
            cl += "\n}\r\n\n";

            return cl;
        }

        private string _getTsTypeString(PropertyDto property, List<PropertyDto> importations = null)
        {
            switch (property.Type)
            {
                case "reference":
                    return property.SchemaName.GetAfterLast(".");
                case "dictionary":
                    return $"{{ [key:{property.GenericArguments[0].Type}]: {_getTsTypeString(property.GenericArguments[1])} }}";
                case "array":
                    if (property.GenericArguments != null && property.GenericArguments.Any())
                    {
                        return $"{_getTsTypeString(property.GenericArguments[0])}[]";

                    }
                    else
                    {
                        return "any[]";
                    }
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
                if (t.IsDecimalType() || t.IsIntgerType())
                    return true;
                if (t.GetGenericTypeDefinition() == typeof(Nullable<>))
                    return false;
                if (t == typeof(string))
                    return false;
                if (typeof(IEnumerable).IsAssignableFrom(t))
                    return false;
            }
            return false;
        }

        public void ExtractDependencies(SchemaItemDto schema, ref Dictionary<string, PropertyDto> dependencies)
        {
            foreach (var prop in schema.Properties)
            {
                if (prop.Value.GenericArguments != null && prop.Value.GenericArguments.Any())
                {
                    foreach (var arg in prop.Value.GenericArguments)
                    {
                        if (!dependencies.ContainsKey(arg.SchemaName) && arg.Type == "reference")
                        {
                            dependencies[arg.SchemaName] = arg;
                        }
                    }
                }

                switch (prop.Value.Type)
                {
                    case "reference":
                        dependencies[prop.Value.SchemaName] = prop.Value;
                        break;
                }
            }
        }

        public string GenerateImportation(Dictionary<string, PropertyDto> props, string ignoreNs)
        {
            Dictionary<string, List<string>> importations = new Dictionary<string, List<string>>();
            foreach (var prop in props)
            {
                var name = prop.Value.SchemaName.GetAfterLast(".");
                if (name == "T")
                {
                    continue;
                }
                if (!importations.ContainsKey(prop.Value.Namespace))
                {
                    importations[prop.Value.Namespace] = new List<string>();
                }
                importations[prop.Value.Namespace].Add(name);
            }

            string result = "";
            foreach (var imp in importations)
            {
                if (imp.Key != ignoreNs)
                {
                    var path = "@" + GetFolderPathFromNamespace(imp.Key) + "/models";

                    result += $"import {{ {string.Join(",", imp.Value)} }} from \"{path}\";\r\n";
                }

            }

            return result;
        }
    }
}
