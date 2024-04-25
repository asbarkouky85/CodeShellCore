using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.PageCategories
{
    public interface IPageCategoryService : IDtoEntityService<long, PageCategoryListDTO, PagedListRequestDto, PageCategoryDto>
    {
        SubmitResult Create(List<PageCategoryDto> list);
        PagedResult<PageCategoryListDTO> GetAll(PagedListRequestDto opt);
        List<TemplateDTO> GetLocalTemplate(IEnumerable<string> files);
        PagedResult<PageCategoryListDTO> GetPagesCategoryByDomain(long domainId, PagedListRequestDto opt);
        List<TemplateDTO> GetTemplates();
    }
}