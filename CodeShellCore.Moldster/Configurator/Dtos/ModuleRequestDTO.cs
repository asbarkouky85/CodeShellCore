using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Configurator.Dtos
{
    public class ModuleRequestDTO
    {
        public string AssemblyName { get; set; }
        public bool? Replace { get; set; }
    }
}
