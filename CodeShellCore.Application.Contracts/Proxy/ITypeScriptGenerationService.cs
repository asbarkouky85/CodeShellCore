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

    }
}