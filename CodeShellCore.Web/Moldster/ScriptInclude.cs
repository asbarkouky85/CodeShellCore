using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Moldster
{
    public enum IncludeType
    {
        Defer,
        Async
    }
    public class ScriptInclude
    {
        public string Script { get; private set; }
        public string Type { get { return Include?.ToString(); } }
        public IncludeType? Include { get; private set; }

        public ScriptInclude(string scr, IncludeType? type = null) {
            Script = scr;
            Include = type;
        }
    }
}
