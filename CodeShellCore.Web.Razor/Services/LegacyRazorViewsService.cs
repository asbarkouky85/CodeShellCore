using CodeShellCore.Data.Mapping;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Views;
using CodeShellCore.Text;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor.Services
{
    public class LegacyRazorViewsService : RazorViewsService
    {
        public LegacyRazorViewsService(IObjectMapper mapper, IMoldsterRazorRenderingService razor, IDataService Data, IHttpContextAccessor contextAccessor) : base(mapper, razor, Data, contextAccessor)
        {
        }

        public override async Task<RenderedPageResultDto> GetPage(PageAcquisitorDTO dto)
        {
            var res = await base.GetPage(dto);
            res.TemplateContent += $"\n<div style='display:none' #lookupOptionsContainer values='{res.Sources.ToJson()}'></div>";
            res.TemplateContent += $"\n<div style='display:none' #viewParamsContainer values='{res.ViewParams.ToJson()}'></div>";
            return res;
        }

        public override async Task<RenderedPageResultDto> GetPageById(long id)
        {
            var res = await base.GetPageById(id);
            res.TemplateContent += $"\n<div style='display:none' #lookupOptionsContainer values='{res.Sources.ToJson()}'></div>";
            res.TemplateContent += $"\n<div style='display:none' #viewParamsContainer values='{res.ViewParams.ToJson()}'></div>";
            return res;
        }
    }
}
