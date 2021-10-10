using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CodeShellCore.Linq
{
    public class LoadResult<T> : LoadResult where T : class
    {
        [IgnoreDataMember]
        public IEnumerable<T> ListT { get { return (List as IEnumerable<T>) ?? new List<T>(); } }

    }
}
