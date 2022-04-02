using CodeShellCore.Data;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Pages;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Data.Repositories
{
    public interface IPageParameterRepository : IRepository<PageParameter>
    {
        IEnumerable<PageParameterForJson> FindForJson(long tenantId, long? pageCategoryId = null);
        IEnumerable<PageParameterForJson> FindForJsonByPage(long pageId);
        LoadResult<PageReferenceDTO> FindReferences(ParameterRequestDTO req, ListOptions<PageReferenceDTO> o);
        List<PageReference> GetReferencesByPage(long id);
    }
}
