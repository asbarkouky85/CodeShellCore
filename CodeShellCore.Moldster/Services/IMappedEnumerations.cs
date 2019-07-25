using System;

namespace CodeShellCore.Moldster.Services
{
    public interface IMappedEnumerations
    {
        bool IsActive { get; }
        string FilePath { get; }
        Func<IScriptGenerationService, string> EnumsGetter { get; }
    }
}
