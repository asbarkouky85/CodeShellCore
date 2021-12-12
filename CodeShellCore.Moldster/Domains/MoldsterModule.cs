using CodeShellCore.Moldster.PageCategories.Dtos;
using System.Collections.Generic;

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
