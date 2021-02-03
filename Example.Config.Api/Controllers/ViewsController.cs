using CodeShellCore.Moldster.Data;
using CodeShellCore.Web.Razor.Controllers;
using CodeShellCore.Web.Razor.Services;

namespace Example.Config.Api.Controllers
{
    public class ViewsController : DbViewsControllerBase
    {
        public ViewsController(ServerViewsService service, IConfigUnit unit) : base(service, unit)
        {
        }
    }
}
