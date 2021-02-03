using CodeShellCore.Data;
using CodeShellCore.Linq;
using CodeShellCore.Linq.Filtering;
using System.Collections.Generic;
using System.Text;

namespace Asga.Dashboard
{
    public interface IDashBoardQuery
    {
        string CollectionId { get; set; }
        IEnumerable<PropertyFilter> Filters { get; set; }
        LoadOptions LoadOptions { get; set; }
    }
}
