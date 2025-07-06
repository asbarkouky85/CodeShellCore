using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency.Analyzer.CodeGeneration;
public class ModuleDefinitionModel
{
    public string Namespace { get; set; }
    public string ClassName { get; set; }
    public string Registrations { get; set; }
    public string Usings { get; set; }
    public string ModuleName { get; set; }
}
