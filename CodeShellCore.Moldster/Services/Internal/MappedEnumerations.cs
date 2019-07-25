using System;

namespace CodeShellCore.Moldster.Services.Internal
{
    public class MappedEnumerations : IMappedEnumerations
    {
        public bool IsActive { get; private set; }
        public MappedEnumerations(Func<IScriptGenerationService, string> func = null)
        {
            IsActive = func != null;
            EnumsGetter = func;
        }

        

        public string FilePath { get; set; }

        public Func<IScriptGenerationService, string> EnumsGetter { get; private set; }
    }
}
