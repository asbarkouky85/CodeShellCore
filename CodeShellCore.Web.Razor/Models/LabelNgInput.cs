using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Models
{
    public class LabelNgInput : NgInput
    {
        public string Pipe { get; set; }
        public string Url { get; set; }
        public bool Blank { get; set; }
    }
}
