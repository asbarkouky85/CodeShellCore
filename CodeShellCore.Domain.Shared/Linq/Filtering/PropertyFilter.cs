using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Linq.Filtering
{
    /// <summary>
    /// Serializer object to facilitate conversion from JSON filter to usable Linq <see cref="Expression"/>
    /// </summary>
    public class PropertyFilter
    {
        /// <summary>
        /// Property to use for filtering
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// int: FilterManager will call <see cref="IFilterManager.GetRangeFilter(string, int, int)"/> &lt;br/&gt;
        /// string: FilterManager will call <see cref="IFilterManager.GetStringContainsFilter(string, string)"/> &lt;br/&gt;
        /// reference: FilterManager will call <see cref="IFilterManager.GetReferenceContainedFilter(string, IEnumerable{long})"/> &lt;br/&gt;
        /// decimal : FilterManager will call <see cref="IFilterManager.GetRangeFilter(string, decimal, decimal)"/> &lt;br/&gt;
        /// date : FilterManager will call <see cref="IFilterManager.GetRangeFilter(string, DateTime, DateTime)"/> &lt;br/&gt;
        /// </summary>
        public string FilterType { get; set; }

        /// <summary>
        /// Depending on the filter type this value can be a string, int, decimal or date
        /// </summary>
        public string Value1 { get; set; }

        /// <summary>
        /// if filter type is int, decimal or date it contains the maximum limit for range, otherwise it will be null
        /// </summary>
        public string Value2 { get; set; }

        /// <summary>
        /// only usable in filter type 'reference'
        /// </summary>
        public IEnumerable<string> Ids { get; set; }

    }
}
