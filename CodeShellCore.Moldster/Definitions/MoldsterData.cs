using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Definitions
{
    public class MoldsterData
    {
        
        public IEnumerable<NgModule> Modules { get; set; }
        public IEnumerable<PageTemplateConfig> Templates { get; set; }
    }
}
