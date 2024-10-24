using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeShellCore.Types;
using CodeShellCore.Text;
using CodeShellCore.Text.Localization;
using CodeShellCore.Helpers;
using System.Text.RegularExpressions;

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
            var foder = ApplyNamingConvension(name_space.Replace(".", "/"));
            foder = foder.Replace("code-shell-core", "codeshell");
            return Utils.CombineUrl("proxy", foder);
        }

        public string ApplyNamingConvension(string path)
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

        private string _getTsTypeString(PropertyDto property)
        {
            switch (property.Type)
            {
                case "reference":
                    var typeName = property.SchemaName.GetAfterLast(".").GetBeforeFirst("`");

                    if (property.GenericArguments != null)
                    {
                        List<string> genericArgs = new List<string>();
                        foreach (var item in property.GenericArguments)
                        {
                            genericArgs.Add(_getTsTypeString(item));
                        }
                        typeName += $"<{string.Join(',', genericArgs)}>";
                    }
                    return typeName;
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
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                return false;
            else if (t.IsDecimalType() || t.IsIntgerType())
                return true;
            else if (t == typeof(string))
                return false;
            else if (typeof(IEnumerable).IsAssignableFrom(t))
                return true;
            return false;
        }



        public string GenerateImportation(Dictionary<string, PropertyDto> props, string ignoreNs = null)
        {
            Dictionary<string, List<string>> importations = new Dictionary<string, List<string>>();
            foreach (var prop in props)
            {
                var name = prop.Value.SchemaName.GetAfterLast(".").GetBeforeFirst("`");
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

        public Dictionary<string, PropertyDto> _extractDependencies(PropertyDto prop, Dictionary<string, PropertyDto> dependencies)
        {
            if (prop.GenericArguments != null && prop.GenericArguments.Any())
            {
                foreach (var arg in prop.GenericArguments)
                {
                    if (!dependencies.ContainsKey(arg.SchemaName) && arg.Type == "reference")
                    {
                        dependencies[arg.SchemaName] = arg;
                    }
                }
            }

            switch (prop.Type)
            {
                case "reference":
                    dependencies[prop.SchemaName] = prop;
                    break;
            }
            return dependencies;
        }

        public Dictionary<string, PropertyDto> ExtractDependencies(SchemaItemDto schema, Dictionary<string, PropertyDto> dependencies)
        {
            foreach (var prop in schema.Properties)
            {
                _extractDependencies(prop.Value, dependencies);
            }
            return dependencies;
        }

        public string MapService(string name, ServiceDefinitionDto value)
        {

            var content = "@Injectable({ providedIn: \"root\" })\r\n";
            content += $"export class {name}Service extends CodeShellProxyService {{ ";
            foreach (var action in value.Actions)
            {
                if (action.Key == "Assign")
                {

                }
                content += "\r\n\r\n";
                var args = _arguments(action.Value.RouteParameters);
                args = _arguments(action.Value.Parameters, args);
                if (action.Value.RequestBody != null)
                    args = _arguments(new Dictionary<string, PropertyDto> { { "bodyData", action.Value.RequestBody } }, args);
                args = args.Distinct().ToList();
                string resultType = "any";
                if (action.Value.Responses.TryGetValue("200", out ResponsePropertyDto responseProperty))
                {
                    resultType = _getTsTypeString(responseProperty);
                }
                content += $"\t{action.Key.LCFirst()}({string.Join(", ", args)}): Observable<{resultType}> {{\r\n";
                content += _generateActionRequest(action.Value, resultType);
                content += "\r\n\t}";
            }
            content += "\r\n}";
            return content;
        }

        string _generateActionRequest(ActionDto action, string resultType)
        {
            var url = action.Path.Replace("{", "${");
            var lines = new List<string>();
            var result = $"\t\t return this.execute<{resultType}>({{";
            result += $"\r\n\t\t\tmethod: '{action.Method.ToLower()}',\r\n\t\t\t";
            lines.Add($"url: `{url}`");

            if (action.Parameters != null && action.Parameters.Any())
            {
                var pars = new List<string>();
                var keys = new List<string>();
                foreach (var argument in action.Parameters)
                {
                    if (argument.Value.Type == "reference")
                    {
                        foreach (var prop in argument.Value.Properties)
                        {
                            var k = prop.Key.LCFirst();
                            if (!keys.Contains(k))
                            {
                                pars.Add($"{k}: {argument.Key}.{k}");
                                keys.Add(k);
                            }
                        }
                    }
                    else
                    {
                        if (!keys.Contains(argument.Key))
                        {
                            pars.Add($"{argument.Key}: {argument.Key}");
                            keys.Add(argument.Key);
                        }
                    }

                }
                lines.Add($"params: {{ {string.Join(", ", pars)} }}");
            }

            if (action.RequestBody != null)
            {
                lines.Add($"body: bodyData");
            }
            result += string.Join(",\r\n\t\t\t", lines);
            result += "\r\n\t\t});";
            return result;
        }

        List<string> _arguments(IDictionary<string, ParameterPropertyDto> props, List<string> arguments = null)
        {
            arguments = arguments ?? new List<string>();
            if (props != null)
            {
                foreach (var pair in props)
                {
                    var name = _getTsTypeString(pair.Value);
                    arguments.Add($"{pair.Key}: {name}");
                }
            }
            return arguments;
        }

        List<string> _arguments(IDictionary<string, PropertyDto> props, List<string> arguments = null)
        {
            arguments = arguments ?? new List<string>();
            if (props != null)
            {
                foreach (var pair in props)
                {
                    var name = _getTsTypeString(pair.Value);
                    if (!arguments.Any(e => e == pair.Key))
                        arguments.Add($"{pair.Key}: {name}");
                }
            }
            return arguments;
        }

        public Dictionary<string, PropertyDto> ExtractDependencies(ServiceDefinitionDto value, Dictionary<string, PropertyDto> dictionary)
        {
            var res = new Dictionary<string, PropertyDto>();
            foreach (var action in value.Actions)
            {
                if (action.Value.RouteParameters != null)
                {
                    foreach (var arg in action.Value.RouteParameters)
                    {
                        _extractDependencies(arg.Value, res);
                    }
                }

                if (action.Value.Parameters != null)
                {
                    foreach (var arg in action.Value.Parameters)
                    {
                        _extractDependencies(arg.Value, res);
                    }
                }

                if (action.Value.Responses != null)
                {
                    foreach (var arg in action.Value.Responses)
                    {
                        _extractDependencies(arg.Value, res);
                    }
                }

                if (action.Value.RequestBody != null)
                {
                    _extractDependencies(action.Value.RequestBody, res);
                }
            }
            return res;
        }
    }
}
