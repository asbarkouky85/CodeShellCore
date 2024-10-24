using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster
{
    public class MoldsterModuleOptions
    {
        public bool ReplaceComponentScripts { get; set; } = true;
        public bool ReplaceComponentHtml { get; set; } = true;
        public bool ReplaceAppComponentHtml { get; set; } = true;
        public bool ReplaceMainRoutes { get; set; } = true;
        public bool ReplaceDomainRoutes { get; set; } = true;
        public bool ReplaceMainModule { get; set; } = true;
        public bool UseLegacy { get; set; } = true;
    }
}
