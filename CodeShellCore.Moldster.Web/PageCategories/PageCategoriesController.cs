using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Web.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor.PageCategories
{

    public class PageCategoriesController : BaseApiController, IPageCategoryService
    {
        IPageCategoryService _service;

        public PageCategoriesController(IPageCategoryService configPageCategoryService)
        {
            _service = configPageCategoryService;
        }

        public Task<SubmitResult> Create(List<PageCategoryDto> list)
        {
            return _service.Create(list);
        }

        public Task<DeleteResult> Delete(long id)
        {
            return _service.Delete(id);
        }

        public Task<PagedResult<PageCategoryListDTO>> Get(PagedListRequestDto options)
        {
            return _service.Get(options);
        }

        public Task<PagedResult<PageCategoryListDTO>> GetAll(PagedListRequestDto opt)
        {
            return _service.GetAll(opt);
        }

        public Task<PagedResult<PageCategoryListDTO>> GetCollection(string id, PagedListRequestDto options)
        {
            return _service.GetCollection(id, options);
        }

        public Task<Dictionary<string, IEnumerable<Named<object>>>> GetEditLookups(Dictionary<string, string> data)
        {
            return _service.GetEditLookups(data);
        }

        public Task<Dictionary<string, IEnumerable<Named<object>>>> GetListLookups(Dictionary<string, string> data)
        {
            return _service.GetListLookups(data);
        }

        public Task<List<TemplateDTO>> GetLocalTemplate(IEnumerable<string> files)
        {
            return _service.GetLocalTemplate(files);
        }

        public Task<PagedResult<PageCategoryListDTO>> GetPagesCategoryByDomain(long domainId, PagedListRequestDto opt)
        {
            return _service.GetPagesCategoryByDomain(domainId, opt);
        }

        public Task<PageCategoryDto> GetSingle(long id)
        {
            return _service.GetSingle(id);
        }

        public Task<List<TemplateDTO>> GetTemplates()
        {
            return _service.GetTemplates();
        }

        public Task<bool> IsUnique(IsUniqueDto dto)
        {
            return _service.IsUnique(dto);
        }

        public Task<EntitySubmitResult<PageCategoryDto>> Post(PageCategoryDto dto)
        {
            return _service.Post(dto);
        }

        public Task<EntitySubmitResult<PageCategoryDto>> Put(PageCategoryDto dto)
        {
            return _service.Put(dto);
        }
    }
}
