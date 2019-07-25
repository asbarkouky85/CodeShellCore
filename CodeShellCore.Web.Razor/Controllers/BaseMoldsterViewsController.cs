using CodeShellCore.Helpers;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Json;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using CodeShellCore.Web.Razor;
using CodeShellCore.Web.Razor.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Razor.Controllers
{
    [ApiExceptionFilter]
    public class BaseMoldsterViewsController : BaseController
    {
        protected readonly IRazorRenderingService Razor;
        protected readonly IDataService Data;

        public BaseMoldsterViewsController(IRazorRenderingService ser, IDataService data)
        {
            Razor = ser;
            Data = data;
        }
        public virtual IActionResult GetPage([FromQuery]PageAcquisitorDTO dto)
        {
            PageOptions p = Data.GetPageOptions(dto.ModuleCode, dto.ViewPath);
            p.Layout = Utils.CombineUrl(RazorConfig.Theme.LayoutBase, p.Layout);
            var html = Razor.RenderPartial(HttpContext, p.ViewPath, null, new Dictionary<string, object> { { "PageOptions", p } });
            html += $"\n<div style='display:none' #lookupOptionsContainer values='{p.SourcesString}'></div>";
            html += $"\n<div style='display:none' #viewParamsContainer values='{p.ViewParamsString}'></div>";

            return Content(html);
        }

        public virtual IActionResult GetMainComponent([FromQuery]PageAcquisitorDTO dto)
        {
            PageOptions p = new PageOptions();

            var html = Razor.RenderPartial(HttpContext, dto.ViewPath, null, new Dictionary<string, object> { { "PageOptions", p } });
            html += $"\n<div style='display:none' #lookupOptionsContainer values='{p.SourcesString}'></div>";
            html += $"\n<div style='display:none' #viewParamsContainer values='{p.ViewParamsString}'></div>";

            return Content(html);
        }
    }
}
