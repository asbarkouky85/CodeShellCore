using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Linq
{
    public abstract class ListOptions
    {
        public bool AsLookup { get; set; }
        public string SearchTerm { get; set; }
        /// <summary>
        /// DESC : for descending &lt;br/&gt;
        /// ASC : for ascending
        /// </summary>
        public SortDir Direction { get; set; }
        
        /// <summary>
        /// starting from (Showing * PageNumber)
        /// </summary>
        public int Skip { get; set; }
        /// <summary>
        /// how many records to show
        /// </summary>
        public int Showing { get; set; }
        /// <summary>
        /// Property for ORDER BY
        /// </summary>
        public string OrderProperty { get; set; }
    }
}
