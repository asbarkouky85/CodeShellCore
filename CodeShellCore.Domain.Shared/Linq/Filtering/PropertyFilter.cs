using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Linq.Filtering
{
    public class PropertyFilter : IPropertyFilter
    {
        public string MemberName { get; set; }
        public string FilterType { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public IEnumerable<string> Ids { get; set; }
    }
}
