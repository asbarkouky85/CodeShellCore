using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.CodeGeneration.Services
{
    public interface IScriptModelMappingService : IServiceBase
    {
        void GenerateDtos(string assembly, bool listItem = false);
        string MapEntity(Type t, bool listItem = false);
        string MapEntity<T>(bool listItem = false) where T : class;
        string MapEnum<T>();
        void ScriptMapping();
    }
}
