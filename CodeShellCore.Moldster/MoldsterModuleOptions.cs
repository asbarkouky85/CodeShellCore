using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster
{
    public class MoldsterModuleOptions
    {
        public bool ReplaceComponentScripts { get; set; }
        public bool ReplaceComponentHtml { get; set; }
        public bool ReplaceAppComponentHtml { get; set; }
        public bool ReplaceMainRoutes { get; set; }
        public bool ReplaceDomainRoutes { get; set; }
        public bool ReplaceMainModule { get; set; }
    }
}
