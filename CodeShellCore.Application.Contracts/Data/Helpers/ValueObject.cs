using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Helpers
{
    public class ValueObject<T>
    {
        public long Id { get; set; }
        public T Value { get; set; }
    }
}
