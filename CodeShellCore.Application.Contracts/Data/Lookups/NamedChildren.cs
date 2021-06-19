using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Lookups
{
    public class NamedChildren
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable Children { get; set; }
    }
}
