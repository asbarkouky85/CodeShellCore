using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor.Models
{
    public class DashboardBlockModel
    {
        public bool SingleItem { get; set; }
        public string Selector { get; set; }
        public string DataField { get; set; }
        public string Attributes { get; set; }
        public string ComponentAttrs { get; set; }
        public int Size { get; set; }
        public string Series { get; set; }
        public string Classes { get; set; }
        public string IsLoadedField { get; set; }
    }
}
