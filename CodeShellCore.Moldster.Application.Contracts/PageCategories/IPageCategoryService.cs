using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Services;
using CodeShellCore.Linq;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.PageCategories
{
    public interface IPageCategoryService : IDtoEntityService<long, PageCategoryListDTO, LoadOptions, PageCategoryDto>
    {
        SubmitResult Create(List<PageCategoryDto> list);
        LoadResult<PageCategoryListDTO> GetAll(LoadOptions opt);
        List<TemplateDTO> GetLocalTemplate(IEnumerable<string> files);
        LoadResult<PageCategoryListDTO> GetPagesCategoryByDomain(long domainId, LoadOptions opt);
        List<TemplateDTO> GetTemplates();
    }
}