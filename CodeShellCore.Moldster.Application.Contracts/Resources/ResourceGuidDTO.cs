using CodeShellCore.Moldster.Pages;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Resources
{
    public class ResourceGuidDTO
    {
        public int PageCount { get; set; }
        public string Name { get; set; }

        public List<PageGuidDTO> Pages { get; set; }

        public IEnumerable<PageGuidDTO> ViewPages { get; set; }
        public IEnumerable<PageGuidDTO> DetailsPages { get; set; }
        public IEnumerable<PageGuidDTO> UpdatePages { get; set; }
        public IEnumerable<PageGuidDTO> InsertPages { get; set; }
        public Dictionary<string, List<PageGuidDTO>> OtherPages { get; set; }
    }
}
