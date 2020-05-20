using System;
using System.Collections;

namespace CodeShellCore.Moldster.Services
{
    public interface IScriptMapping
    {
        bool IsActive { get; }
        string FilePath { get; }
        Func<IScriptModelMappingService, string> FileWriter { get; }
        
    }
}
