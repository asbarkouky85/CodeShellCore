using System.Collections.Generic;

namespace CodeShellCore.Moldster.Pages
{
    public class PageGuidDTO
    {
        public string Name { get; set; }
        public string PrivilegeName { get; set; }
        public string AppsString { get; set; }
        public IEnumerable<string> Apps { get { return AppsString.Split(','); } }
    }
}
