using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Razor
{
    public class ViewParams
    {
        //public string ModelType { get; set; }
        public string AddUrl { get; set; }
        public string EditUrl { get; set; }
        public string DetailsUrl { get; set; }
        public string ListUrl { get; set; }
        public IEnumerable<FieldDefinition> Fields { get; set; } = new List<FieldDefinition>();
        
        public Dictionary<string, string> Other { get; set; }
        public ViewParams()
        {
            Other = new Dictionary<string, string>();
        }
        
        public string GetFromOther(string st,string defaultValue=null)
        {
            if (Other.TryGetValue(st, out string o))
                return o;
            else if (defaultValue != null)
                return defaultValue;
            return "";
        }
    }
}
