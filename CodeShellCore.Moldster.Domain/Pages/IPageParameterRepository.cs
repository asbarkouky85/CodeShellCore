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
        LoadResult<PageReferenceView> FindReferences(ParameterRequest req, ListOptions<PageReferenceView> o);
        List<PageReference> GetReferencesByPage(long id);
    }
}
