using CodeShellCore.Moldster.Resources;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Domains
{
    public class DomainGuidDTO
    {
        public string Name { get; set; }

        public List<ResourceGuidDTO> Resources { get; set; }
    }
}
