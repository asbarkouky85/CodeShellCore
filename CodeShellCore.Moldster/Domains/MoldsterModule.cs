using CodeShellCore.Moldster.Configurator.Dtos;
using CodeShellCore.Moldster.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Domains
{
    public class MoldsterModule
    {
        public string Name { get; set; }
        public string AssemblyName { get; set; }
        public string InstallPath { get; set; }
        public IEnumerable<ModuleCategoryDTO> Categories { get; set; }
        public IEnumerable<string> Resources { get; set; }
    }


}
