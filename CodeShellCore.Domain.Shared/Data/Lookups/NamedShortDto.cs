using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Lookups
{
    public class NamedShortDto<T> : Named<T> 
    {
        public string ShortName { get; set; }
    }
}
