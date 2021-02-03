using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Moldster.Data;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Moldster.Razor.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Web.Razor.Services
{
    public class ServerViewsService : MoldsterRazorService, IViewsService
    {
        private readonly IConfigUnit _unit;
        private readonly IDataService data;
        private readonly IHttpContextAccessor contextAccessor;

        public ServerViewsService(
            IRazorViewEngine engine,
            ITempDataProvider tmp,
            IConfigUnit unit,
            IDataService Data,
            IHttpContextAccessor contextAccessor
            ) : base(engine, tmp)
        {
            this._unit = unit;
            data = Data;
            this.contextAccessor = contextAccessor;
        }

        public bool CheckServer(out HttpResult res)
        {
            res = new HttpResult(System.Net.HttpStatusCode.OK);
            return true;
        }

        public string GetGuide(string moduleCode)
        {
            long id = _unit.TenantRepository.GetSingleValue(d => d.Id, d => d.Code == moduleCode);
            TenantPageGuideDTO sin = data.GetAppGuide(id);
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
            return RenderPartial(contextAccessor.HttpContext, "Auth/Guide", sin);
        }

        public string GetMainComponent(string baseComponent)
        {
            PageOptions p = new PageOptions();

            var html = RenderPartial(contextAccessor.HttpContext, baseComponent, null, new Dictionary<string, object> { { "PageOptions", p } });
            html += $"\n<div style='display:none' #lookupOptionsContainer values='{p.SourcesString}'></div>";
            html += $"\n<div style='display:none' #viewParamsContainer values='{p.ViewParamsString}'></div>";
            return html;
        }

        public string GetPage(PageAcquisitorDTO dto)
        {
            PageOptions p = data.GetPageOptions(dto.ModuleCode, dto.ViewPath);
            p.Layout = Utils.CombineUrl(RazorConfig.Theme.BasePath, p.Layout);
            var html = RenderPartial(contextAccessor.HttpContext, p.ViewPath, null, new Dictionary<string, object> { { "PageOptions", p } });
            html += $"\n<div style='display:none' #lookupOptionsContainer values='{p.SourcesString}'></div>";
            html += $"\n<div style='display:none' #viewParamsContainer values='{p.ViewParamsString}'></div>";
            return html;
        }

        public string GetPageById(long id)
        {
            PageOptions p = data.GetPageOptionsById(id);
            p.Layout = Utils.CombineUrl(RazorConfig.Theme.BasePath, p.Layout);
            var html = RenderPartial(contextAccessor.HttpContext, p.ViewPath, null, new Dictionary<string, object> { { "PageOptions", p } });
            html += $"\n<div style='display:none' #lookupOptionsContainer values='{p.SourcesString}'></div>";
            html += $"\n<div style='display:none' #viewParamsContainer values='{p.ViewParamsString}'></div>";
            return html;
        }

        public TemplateDataCollector GetTemplateData(long id)
        {
            var path = _unit.PageCategoryRepository.GetValue(id, d => new { d.ViewPath, d.BaseComponent, d.Layout });

            string layout = null;
            if (path.Layout != null)
                layout = Utils.CombineUrl(RazorConfig.Theme.BasePath, "Layout", path.Layout + "Layout.cshtml");
            else if ((new string[] { "Edit", "List" }).Contains(path.BaseComponent))
                layout = Utils.CombineUrl(RazorConfig.Theme.BasePath, "Layout", path.BaseComponent + "Layout.cshtml");
            return GetCollector(contextAccessor.HttpContext, path.ViewPath, layout);
        }
    }
}
