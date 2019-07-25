using CodeShellCore.Moldster.Db.Services;
using CodeShellCore.Web.Razor.Controllers;
using CodeShellCore.Web.Filters;

namespace ExampleProject.Config.Api.Controllers
{
    [ApiAuthorize(AllowAnonymous = true)]
    public class PagesController : PagesControllerBase
    {
        public PagesController(PagesService service) : base(service)
        {
        }
    }
}