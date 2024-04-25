using CodeShellCore.Data;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Pages.Views;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageParameterRepository : IRepository<PageParameter>
    {
        IEnumerable<PageParameterForJson> FindForJson(long tenantId, long? pageCategoryId = null);
        IEnumerable<PageParameterForJson> FindForJsonByPage(long pageId);
        PagedResult<PageReferenceView> FindReferences(ParameterRequest req, PagedListRequest<PageReferenceView> o);
        List<PageReference> GetReferencesByPage(long id);
    }
}
