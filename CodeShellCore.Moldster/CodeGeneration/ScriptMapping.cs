using CodeShellCore.Moldster.CodeGeneration.Services;
using System;

namespace CodeShellCore.Moldster.CodeGeneration
{
    public class ScriptMapping : IScriptMapping
    {
        public bool IsActive { get; private set; }
        public ScriptMapping(Func<IScriptModelMappingService, string> func = null)
        {
            IsActive = func != null;
            FileWriter = func;
        }



        public string FilePath { get; set; }

        public Func<IScriptModelMappingService, string> FileWriter { get; private set; }
    }
}
