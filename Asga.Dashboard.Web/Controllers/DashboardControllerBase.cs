using Asga.Dashboard.Business;
using CodeShellCore.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace Asga.Dashboard.Web.Controllers
{
    public class DashboardControllerBase<T> : CodeShellCore.Web.Controllers.BaseApiController
        where T : class, IDashBoardQuery
    {
        protected IDashboardItemService<T> Service => GetService<IDashboardItemService<T>>();
        public IActionResult GetItem(string listName, [FromBody]T query)
        {
            query.Process();
            var res = Service.GetItem(listName, query);
            foreach (var it in res.ListT)
                it.ListName = it.ListName ?? listName;
            return Respond(res);
        }
    }
}
