using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Lookups
{
    public class NamedChildren : NamedDto<object>
    {
        public IEnumerable Children { get; set; }
    }
}
