using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Localization
{
    public class AbpResourceFile 
    {
        public string Culture { get; set; }
        public Dictionary<string,string> Texts { get; set; }
    }
}
