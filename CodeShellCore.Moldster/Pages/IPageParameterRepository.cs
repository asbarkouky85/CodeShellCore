using CodeShellCore.Data;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Pages.Dtos;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Data.Repositories
{
    public interface IPageParameterRepository : IRepository<PageParameter>
    {
        List<PageParameterDTO> FindForPage(long id);
        IEnumerable<PageParameterForJson> FindForJson(long tenantId, long? pageCategoryId = null);
        IEnumerable<PageParameterForJson> FindForJsonByPage(long pageId);
        LoadResult<PageReferenceDTO> FindReferences(ParameterRequestDTO req, ListOptions<PageReferenceDTO> o);
    }
}
