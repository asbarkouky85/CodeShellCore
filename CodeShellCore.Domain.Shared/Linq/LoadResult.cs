using System.Collections;
using System.Collections.Generic;

namespace CodeShellCore.Linq
{
    public class LoadResult
    {
        public int TotalCount { get; set; }
        public IEnumerable List { get; set; } = new object[0];

        public static LoadResult Empty
        {
            get
            {
                return new LoadResult
                {
                    TotalCount = 0,
                    List = new List<object>()
                };
            }
        }
    }
}
