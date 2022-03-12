using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Pages.Dtos;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CodeShellCore.Web.Razor.Services
{
    public class LegacyRazorViewsService : RazorViewsService
    {
        public LegacyRazorViewsService(
            IRazorRenderingService razor,
            IConfigUnit unit,
            IDataService Data,
            IHttpContextAccessor contextAccessor) : base(razor, unit, Data, contextAccessor)
        {
        }

        public override RenderedPageResult GetPage(PageAcquisitorDTO dto)
        {
            var res = base.GetPage(dto);
            res.TemplateContent += $"\n<div style='display:none' #lookupOptionsContainer values='{res.Sources.ToJson()}'></div>";
            res.TemplateContent += $"\n<div style='display:none' #viewParamsContainer values='{res.ViewParams.ToJson()}'></div>";
            return res;
        }

        public override RenderedPageResult GetPageById(long id)
        {
            var res = base.GetPageById(id);
            res.TemplateContent += $"\n<div style='display:none' #lookupOptionsContainer values='{res.Sources.ToJson()}'></div>";
            res.TemplateContent += $"\n<div style='display:none' #viewParamsContainer values='{res.ViewParams.ToJson()}'></div>";
            return res;
        }
    }
}
