using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.Modularity
{
    public class GenerateModuleClassesReplaceModel
    {
        public string ClassName { get; set; }
        public string Usings { get; set; }
        public string Dependencies { get; set; }
        public string Namespace { get; set; }
        public string ModuleName { get; set; }
        public string Registrations { get; set; }
    }
}
