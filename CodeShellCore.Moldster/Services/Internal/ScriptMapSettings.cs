using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Services.Internal
{
    public class ScriptMapSettings
    {
        static List<IScriptMapping> _mappings = new List<IScriptMapping>();
       public static IEnumerable<IScriptMapping> Mappings { get { return _mappings; } }
        public static void Add(IScriptMapping mapping)
        {
            _mappings.Add(mapping);
        }
    }
}
