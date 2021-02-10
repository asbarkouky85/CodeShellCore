using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Models
{
    public class RadioNgInput : NgInput
    {
        public Dictionary<string, object> Values { get; set; }
        public bool Enabled { get; set; }
    }
}
