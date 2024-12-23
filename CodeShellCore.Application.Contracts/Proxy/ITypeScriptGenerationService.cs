using System;
using System.Collections.Generic;

namespace CodeShellCore.Proxy
{
    public interface ITypeScriptGenerationService
    {
        string GetTsType(Type t);
        bool IsRequired(Type t);
        string GenerateEnum(string name, Dictionary<string, long> values);
        string GenerateModel(SchemaItemDto schema);
        string GenerateService(string name, ServiceDefinitionDto value);
        string ApplyNamingConvension(string path);
        string GetFolderPathFromNamespace(string name_space);
        Dictionary<string, PropertyDto> ExtractDependencies(SchemaItemDto schema, Dictionary<string, PropertyDto> dependencies);
        string GenerateImportation(Dictionary<string, PropertyDto> props, string ignoreNs = null);
        Dictionary<string, PropertyDto> ExtractDependencies(ServiceDefinitionDto value, Dictionary<string, PropertyDto> dictionary);
    }
}