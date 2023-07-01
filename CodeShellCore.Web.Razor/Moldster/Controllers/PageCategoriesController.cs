using CodeShellCore.Data.Helpers;
using CodeShellCore.Data.Lookups;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CodeShellCore.Moldster.PageCategories.Services;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.PageCategories;

namespace CodeShellCore.Web.Razor.Moldster.Controllers
{

    public class PageCategoriesController : BaseApiController, IPageCategoryService
    {
        IPageCategoryService _service;

        public PageCategoriesController(IPageCategoryService configPageCategoryService)
        {
            _service = configPageCategoryService;
        }

        public List<TemplateDTO> GetTemplate()
        {
            return _service.GetTemplates();
        }

        public SubmitResult CreatPageCategory([FromBody] List<PageCategoryDto> pageCategories)
        {
            return _service.Create(pageCategories);
        }

        public SubmitResult<PageCategoryDto> EditPageCategory([FromBody] PageCategoryDto pageCategory)
        {
            return _service.Put(pageCategory);
        }

        public Dictionary<string, IEnumerable<Named<object>>> GetEditLookups([FromQuery] Dictionary<string, string> data)
        {

            return _service.GetEditLookups(data);
        }

        public Dictionary<string, IEnumerable<Named<object>>> GetListLookups([FromQuery] Dictionary<string, string> data)
        {
            return _service.GetListLookups(data);
        }

        public SubmitResult Create([FromBody] List<PageCategoryDto> list)
        {
            return _service.Create(list);
        }

        public LoadResult<PageCategoryListDTO> GetAll([FromQuery] LoadOptions opt)
        {
            return _service.GetAll(opt);
        }

        public List<TemplateDTO> GetLocalTemplate([FromBody] IEnumerable<string> files)
        {
            return _service.GetLocalTemplate(files);
        }

        public LoadResult<PageCategoryListDTO> GetPagesCategoryByDomain(long domainId, [FromQuery] LoadOptions opt)
        {
            return _service.GetPagesCategoryByDomain(domainId, opt);
        }

        public List<TemplateDTO> GetTemplates()
        {
            return _service.GetTemplates();
        }

        public DeleteResult Delete(long id)
        {
            return _service.Delete(id);
        }

        public SubmitResult<PageCategoryDto> Post([FromBody] PageCategoryDto dto)
        {
            return _service.Post(dto);
        }

        public SubmitResult<PageCategoryDto> Put([FromBody] PageCategoryDto dto)
        {
            return _service.Put(dto);
        }

        public LoadResult<PageCategoryListDTO> Get([FromQuery] LoadOptions options)
        {
            return _service.Get(options);
        }

        public PageCategoryDto GetSingle(long id)
        {
            return _service.GetSingle(id);
        }

        public bool IsUnique([FromQuery] IsUniqueDto dto)
        {
            return _service.IsUnique(dto);
        }

        public LoadResult<PageCategoryListDTO> GetCollection(string id, [FromQuery] LoadOptions options)
        {
            return _service.GetCollection(id, options);
        }
    }
}
