using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Models
{
    public class FilterModel
    {
        public string FilterName { get; set; }
        public string Property { get; set; }
        public string Id { get { return FilterName + "_" + Property; } }
    }
}
