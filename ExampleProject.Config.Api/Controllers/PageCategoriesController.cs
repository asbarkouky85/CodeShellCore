using CodeShellCore.Data.Services;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Moldster.Db;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.Config.Api.Controllers
{
    [ApiAuthorize(AllowAnonymous = true)]
    public class PageCategoriesController : EntityController<PageCategory,long>,IEntityController<PageCategory,long>
    {
        public PageCategoriesController(IEntityService<PageCategory> service) : base(service)
        {
        }

        public IActionResult Post([FromBody] PageCategory obj)
        {
            return DefaultPost(obj);
        }

        public IActionResult Put([FromBody] PageCategory obj)
        {
            return DefaultPut(obj);
        }
    }
}
