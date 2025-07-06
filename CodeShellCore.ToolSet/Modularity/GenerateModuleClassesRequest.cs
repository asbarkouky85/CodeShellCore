using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.ToolSet.Modularity
{
    public enum Frameworks
    {
        None, Abp, Codeshell
    }
    public class GenerateModuleClassesRequest
    {
        public string SolutionFolder { get; set; }
        public Frameworks Type { get; set; }
    }
}
