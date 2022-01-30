using CodeShellCore.Moldster.Data;
using CodeShellCore.Web.Razor.Controllers;
using CodeShellCore.Web.Razor.Services;

namespace GenerationTest.Api.Controllers
{
    public class ViewsController : DbViewsControllerBase
    {
        public ViewsController(
            ServerViewsService views,
            IConfigUnit unit)
            : base(views,unit)
        {
        }
    }
}
