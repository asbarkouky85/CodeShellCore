using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CodeShellCore.Linq
{
    public class PagedResult<T> where T : class
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> List { get; set; }

    }
}
