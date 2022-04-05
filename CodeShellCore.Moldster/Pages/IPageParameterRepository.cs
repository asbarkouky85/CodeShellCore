using CodeShellCore.Data;
using CodeShellCore.Linq;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Pages
{
    public interface IPageParameterRepository : IRepository<PageParameter>
    {
        IEnumerable<PageParameterForJson> FindForJson(long tenantId, long? pageCategoryId = null);
        IEnumerable<PageParameterForJson> FindForJsonByPage(long pageId);
        LoadResult<PageReferenceDTO> FindReferences(ParameterRequestDTO req, ListOptions<PageReferenceDTO> o);
        List<PageReference> GetReferencesByPage(long id);
    }
}
