using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Db.Data;
using CodeShellCore.Moldster.Razor.Services;
using CodeShellCore.Web.Razor;
using CodeShellCore.Helpers;

namespace CodeShellCore.Web.Razor.Controllers
{

    
    public class DbViewsControllerBase : BaseMoldsterViewsController
    {
        private readonly IConfigUnit _unit;
        readonly IMoldsterRazorService _raz;
        public DbViewsControllerBase(IMoldsterRazorService ser, IDataService data, IConfigUnit unit):base(ser,data)
        {
            _unit = unit;
            _raz = ser;
        }

        

        public JsonResult GetTemplateData(long id)
        {

            var path = _unit.PageCategoryRepository.GetValue(id, d => new { d.ViewPath, d.BaseComponent, d.Layout });

            string layout = null;
            if (path.Layout != null)
                layout = Utils.CombineUrl(RazorConfig.Theme.LayoutBase,"Layout", path.Layout + "Layout.cshtml");
            else if ((new string[] { "Edit", "List" }).Contains(path.BaseComponent))
                layout = Utils.CombineUrl(RazorConfig.Theme.LayoutBase, "Layout", path.BaseComponent + "Layout.cshtml");
            return Json(_raz.GetCollector(HttpContext, path.ViewPath, layout));


        }
    }
}
