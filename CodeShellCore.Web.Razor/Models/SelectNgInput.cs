using CodeShellCore.Data.Lookups;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Models
{
    public class SelectNgInput : NgInput
    {
        public string Display { get; set; }
        public string ValueMember { get; set; }
        public string Value { get { return ValueMember == null ? "" : "." + ValueMember; } }
        public string ListName { get; set; }
        public bool Multi { get; set; }
        public string ChangeFunction { get; set; }
        public bool Nullable { get; set; }
        public IEnumerable<Named<long>> Choices { get; set; }
    }
}
