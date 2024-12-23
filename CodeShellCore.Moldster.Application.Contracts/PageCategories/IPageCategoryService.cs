using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.PageCategories
{
    public interface IPageCategoryService : IDtoEntityService<long, PageCategoryListDTO, PagedListRequestDto, PageCategoryDto>
    {
        Task<SubmitResult> Create(List<PageCategoryDto> list);
        Task<PagedResult<PageCategoryListDTO>> GetAll(PagedListRequestDto opt);
        Task<List<TemplateDTO>> GetLocalTemplate(IEnumerable<string> files);
        Task<PagedResult<PageCategoryListDTO>> GetPagesCategoryByDomain(long domainId, PagedListRequestDto opt);
        Task<List<TemplateDTO>> GetTemplates();
    }
}