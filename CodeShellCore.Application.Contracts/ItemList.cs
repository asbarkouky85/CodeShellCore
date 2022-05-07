using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore
{
    public class ItemList<T>
    {
        public IEnumerable<T> items { get; set; }
    }
}
