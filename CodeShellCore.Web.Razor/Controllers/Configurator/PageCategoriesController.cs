using CodeShellCore.Linq;
using CodeShellCore.Moldster.Configurator.Services;
using CodeShellCore.Moldster;
using CodeShellCore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CodeShellCore.Moldster.PageCategories.Services;

namespace CodeShellCore.Web.Razor.Controllers.Configurator
{

    public class PageCategoriesController : EntityController<PageCategory, long>, IEntityController<PageCategory, long>, ILookupLoaderController
    {
        PageCategoryService _service;
        ConfiguratorLookupService Lookups => GetService<ConfiguratorLookupService>();
        public PageCategoriesController(PageCategoryService configPageCategoryService) : base(configPageCategoryService)
        {
            _service = configPageCategoryService;
        }

        public IActionResult Post([FromBody] PageCategory obj)
        {
            return DefaultPost(obj);
        }

        public IActionResult Put([FromBody] PageCategory obj)
        {
            return DefaultPut(obj);
        }


        public override IActionResult Get([FromQuery] LoadOptions opt)
        {
            return Respond(_service.GetAll(opt));
        }

        public IActionResult GetPagesCategoryByDomain([FromQuery] LoadOptions opts, [FromQuery] long domainId)
        {
            return Respond(_service.GetPagesCategoryByDomain(domainId, opts));
        }

        public IActionResult GetTemplate()
        {
            return Respond(_service.GetTemplates());
        }

        public IActionResult CreatPageCategory([FromBody] List<PageCategory> pageCategories)
        {
            return Respond(_service.Create(pageCategories));
        }

        public IActionResult EditPageCategory([FromBody] PageCategory pageCategory)
        {
            return Respond(_service.Update(pageCategory));
        }

        public IActionResult GetEditLookups([FromQuery] Dictionary<string, string> data)
        {
            object lu = Lookups.PageCategoryEdit(data);
            return Respond(lu);
        }

        public IActionResult GetListLookups([FromQuery] Dictionary<string, string> data)
        {
            object lu = Lookups.PageCategoryEdit(data);
            return Respond(lu);
        }
    }
}
