using System.Collections.Generic;
using CodeShellCore.Text;
using CodeShellCore.Moldster.Dto;

namespace CodeShellCore.Moldster.Razor
{
    public class PageOptions
    {
        private ViewParams _viewParams;
        public long PageId { get; set; }
        public string ViewPath { get; set; }
        public string PageIdentifier { get; set; }
        public Dictionary<string, ControlDTO> Controls { get; set; }
        public IEnumerable<string> RepeatedIds { get; set; }
        public List<Lister> Sources { get; set; }
        public string ViewParamsString { get; set; }
        public string Layout { get; set; }
        public int DefaultAccessibility { get; set; }

        /// <summary>
        /// if not existant returns empty instance of ViewParams
        /// </summary>
        public ViewParams ViewParams
        {
            get
            {
                if (_viewParams == null)
                    _viewParams = ViewParamsString == null ? new ViewParams() : ViewParamsString.FromJson<ViewParams>();
                return _viewParams;
            }
        }
        public PageOptions()
        {
            Sources = new List<Lister>();
            Controls = new Dictionary<string, ControlDTO>();
        }
        public string SourcesString
        {
            get
            {
                return GetSourcesString()?.ToJson();
            }
        }

        public Dictionary<string,string> GetSourcesString()
        {
            Dictionary<string, string> items = new Dictionary<string, string>();
            if (Sources.Count == 0)
                return null;
            foreach (Lister l in Sources)
                items[l.ListName] = l.CollecionName;
            return items;
        }

        public Accessibility GetAccessibility(string name)
        {
            if (!Controls.TryGetValue(name, out ControlDTO opts))
                return new Accessibility(0);

            return new Accessibility(opts.Accessibilty);
        }
    }
}
