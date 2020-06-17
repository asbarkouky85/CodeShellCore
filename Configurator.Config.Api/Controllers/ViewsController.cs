using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Moldster.Razor.Services;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Web.Razor.Controllers;
using CodeShellCore.Web.Razor.Services;

namespace Configurator.Config.Api.Controllers
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
