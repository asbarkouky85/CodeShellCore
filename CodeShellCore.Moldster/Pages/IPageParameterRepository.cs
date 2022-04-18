using CodeShellCore.Data;
using CodeShellCore.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Moldster.Pages.Dtos;
using CodeShellCore.Moldster.Pages;

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
