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
using System.Threading.Tasks;
using CodeShellCore.Moldster.Db.Dto;

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

        public Task<ContentResult> GetGuide(long id)
        {
            return Task.Run(() =>
            {
                TenantPageGuideDTO sin = Data.GetTenantGuide(id);
                var def = new string[] { "view", "details", "update", "insert" };
                foreach (var d in sin.Domains)
                {
                    foreach (var r in d.Resources)
                    {
                        r.ViewPages = r.Pages.Where(p => p.PrivilegeName == "view");
                        r.DetailsPages = r.Pages.Where(p => p.PrivilegeName == "details");
                        r.UpdatePages = r.Pages.Where(p => p.PrivilegeName == "update");
                        r.InsertPages = r.Pages.Where(p => p.PrivilegeName == "insert");
                        r.OtherPages = new Dictionary<string, List<PageGuidDTO>>();
                        var ps = r.Pages.Where(p => !def.Contains(p.PrivilegeName));
                        foreach (var p in ps)
                        {

                            if (!r.OtherPages.TryGetValue(p.PrivilegeName, out List<PageGuidDTO> data))
                            {
                                r.OtherPages[p.PrivilegeName] = new List<PageGuidDTO>();
                            }
                            r.OtherPages[p.PrivilegeName].Add(p);
                        }
                        r.Pages = null;
                    }
                }
                string view = Razor.RenderPartial(HttpContext, "Auth/Guide", sin);

                return Content(view, "text/html");
            });

        }
    }
}
