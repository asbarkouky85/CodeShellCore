using CodeShellCore.Linq.Filtering;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CodeShellCore.Linq
{
    public class PagedListRequest
    {
        public bool AsLookup { get; set; }
        public string SearchTerm { get; set; }
        public SortDir Direction { get; set; }
        public int Skip { get; set; }
        public int Showing { get; set; }
        public string OrderProperty { get; set; }
        public IEnumerable<PropertyFilter> PropertyFilters { get; set; } = new List<PropertyFilter>();

    }
}
