using CodeShellCore.Data;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Pages.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageParameterRepository : IRepository<PageParameter>
    {
        Task<IEnumerable<PageParameterForJson>> FindForJson(long tenantId, long? pageCategoryId = null);
        Task<IEnumerable<PageParameterForJson>> FindForJsonByPage(long pageId);
        Task<PagedResult<PageReferenceView>> FindReferences(ParameterRequest req, PagedListRequest<PageReferenceView> o);
        Task<List<PageReference>> GetReferencesByPage(long id);
    }
}
