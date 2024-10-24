using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Views;
using CodeShellCore.Text;
using Microsoft.AspNetCore.Http;
using System;

namespace CodeShellCore.Web.Razor.Services
{
    public class LegacyRazorViewsService : RazorViewsService
    {
        public LegacyRazorViewsService(
            IServiceProvider serviceProvider,
            IMoldsterRazorRenderingService razor,
            IConfigUnit unit,
            IDataService Data,
            IHttpContextAccessor contextAccessor) : base(serviceProvider, razor, unit, Data, contextAccessor)
        {
        }

        public override RenderedPageResultDto GetPage(PageAcquisitorDTO dto)
        {
            var res = base.GetPage(dto);
            res.TemplateContent += $"\n<div style='display:none' #lookupOptionsContainer values='{res.Sources.ToJson()}'></div>";
            res.TemplateContent += $"\n<div style='display:none' #viewParamsContainer values='{res.ViewParams.ToJson()}'></div>";
            return res;
        }

        public override RenderedPageResultDto GetPageById(long id)
        {
            var res = base.GetPageById(id);
            res.TemplateContent += $"\n<div style='display:none' #lookupOptionsContainer values='{res.Sources.ToJson()}'></div>";
            res.TemplateContent += $"\n<div style='display:none' #viewParamsContainer values='{res.ViewParams.ToJson()}'></div>";
            return res;
        }
    }
}
