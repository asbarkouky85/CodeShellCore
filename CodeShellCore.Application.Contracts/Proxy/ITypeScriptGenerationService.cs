using System;
using System.Collections.Generic;

namespace CodeShellCore.Proxy
{
    public interface ITypeScriptGenerationService
    {
        string GetTsType(Type t);
        bool IsRequired(Type t);
        string MapEnum(string name, Dictionary<string, long> values);
        string MapEntity(SchemaItemDto schema);
        string GetFolderPathFromNamespace(string name_space);
        void ExtractDependencies(SchemaItemDto schema, ref Dictionary<string, PropertyDto> dependencies);
        string GenerateImportation(Dictionary<string, PropertyDto> props, string ignoreNs);
    }
}