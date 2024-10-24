using System.Collections.Generic;
using CodeShellCore.Text;
using CodeShellCore.Moldster.PageCategories;

namespace CodeShellCore.Moldster.Pages
{
    public class PageOptions
    {
        public long PageId { get; set; }
        public string ViewPath { get; set; }
        public string PageIdentifier { get; set; }
        public Dictionary<string, ControlRenderDataObject> Controls { get; set; }
        public IEnumerable<string> RepeatedIds { get; set; }
        public List<Lister> Sources { get; set; }
        public string ViewParamsString { get; set; }
        public string Layout { get; set; }
        public int DefaultAccessibility { get; set; }

        public PageOptions()
        {
            Sources = new List<Lister>();
            Controls = new Dictionary<string, ControlRenderDataObject>();
        }



    }
}
