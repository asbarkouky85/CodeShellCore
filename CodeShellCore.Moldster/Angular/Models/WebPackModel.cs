using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Models
{
    public class WebPackModel
    {
        public string Tenants { get; set; }
        public string Code { get; set; }
        public string Lazy { get; set; }
        public string LazyLower { get { return Lazy?.ToLower(); } }
    }
}
