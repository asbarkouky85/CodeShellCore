using CodeShellCore.Data.Lookups;
using CodeShellCore.Data.Mapping;
using CodeShellCore.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Data
{
    public interface IRepository
    {
        Task<int> Count();
        Task<IEnumerable<Named<object>>> FindAsLookup(string collectionId = null);
        Task<PagedResult<Named<object>>> FindAsLookupPaged(PagedListRequest request, string collectionId = null);
        Task<IEnumerable> All();
        IQueryProjector Projector { get; set; }

    }


}
