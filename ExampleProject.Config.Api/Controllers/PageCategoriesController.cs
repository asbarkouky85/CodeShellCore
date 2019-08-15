using CodeShellCore.Web.Filters;
using CodeShellCore.Web.Razor.Controllers;
using CodeShellCore.Moldster.Db.Services;

namespace ExampleProject.Config.Api.Controllers
{
    [ApiAuthorize(AllowAnonymous = true)]
    public class PageCategoriesController : PageCategoriesControllerBase
    {
        public PageCategoriesController(PageCategoryService service) : base(service)
        {
        }
        
    }
}
