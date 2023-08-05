using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Lookups
{
    public class NamedChildren : Named<object>
    {
        public IEnumerable Children { get; set; }
    }
}
