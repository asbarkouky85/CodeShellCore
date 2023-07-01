using CodeShellCore.Moldster.CodeGeneration.Services;
using System;

namespace CodeShellCore.Moldster.CodeGeneration
{
    public interface IScriptMapping
    {
        bool IsActive { get; }
        string FilePath { get; }
        Func<IScriptModelMappingService, string> FileWriter { get; }

    }
}
