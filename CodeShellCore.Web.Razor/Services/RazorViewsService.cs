using CodeShellCore.Data.Mapping;
using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Services;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Moldster.Views;
using CodeShellCore.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor.Services
{
    public class RazorViewsService : IViewsService
    {
        private readonly IMoldsterRazorRenderingService razor;
        private readonly IDataService data;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IObjectMapper Mapper;
        public RazorViewsService(
            IObjectMapper mapper,
            IMoldsterRazorRenderingService razor,
            IDataService Data,
            IHttpContextAccessor contextAccessor
            )
        {
            Mapper = mapper;
            this.razor = razor;
            data = Data;
            this.contextAccessor = contextAccessor;
        }

        public virtual bool CheckServer(out HttpResult res)
        {
            res = new HttpResult(System.Net.HttpStatusCode.OK);
            return true;
        }

        public virtual async Task<string> GetGuide(string moduleCode)
        {
            long id = await data.GetTenantIdByCode(moduleCode);

            TenantPageGuideDTO sin = await data.GetAppGuide(id);
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
            return await razor.RenderPartial(contextAccessor.HttpContext, "Auth/Guide", sin);
        }

        public virtual async Task<string> GetMainComponent(string baseComponent)
        {
            PageOptionsDto p = new PageOptionsDto();

            var html = await razor.RenderPartial(contextAccessor.HttpContext, baseComponent, null, new Dictionary<string, object> { { nameof(PageOptionsDto), p } });
            html += $"\n<div style='display:none' #lookupOptionsContainer values='{p.SourcesString}'></div>";
            html += $"\n<div style='display:none' #viewParamsContainer values='{p.ViewParamsString}'></div>";
            return html;
        }

        public virtual async Task<RenderedPageResultDto> GetPage(PageAcquisitorDTO dto)
        {
            PageOptionsDto p = await data.GetPageOptions(dto.ModuleCode, dto.ViewPath);
            p.Layout = Utils.CombineUrl(RazorConfig.Theme.BasePath, p.Layout);
            var html = await razor.RenderPartial(contextAccessor.HttpContext, p.ViewPath, null, new Dictionary<string, object> { { nameof(PageOptionsDto), p } });
            return new RenderedPageResultDto
            {
                TemplateContent = html,
                Sources = p.GetSourcesString(),
                ViewParams = p.ViewParams
            };
        }

        public virtual async Task<RenderedPageResultDto> GetPageById(long id)
        {
            PageOptionsDto p = await data.GetPageOptionsById(id);
            p.Layout = Utils.CombineUrl(RazorConfig.Theme.BasePath, p.Layout);
            var html = await razor.RenderPartial(contextAccessor.HttpContext, p.ViewPath, null, new Dictionary<string, object> { { nameof(PageOptionsDto), p } });
            return new RenderedPageResultDto
            {
                TemplateContent = html,
                Sources = p.GetSourcesString(),
                ViewParams = p.ViewParams
            };
        }

        public async Task<RenderedPageResultDto> GetPageCategoryById(long id)
        {
            PageOptionsDto p = await data.GetCategoryPageOptions(id);
            p.Layout = Utils.CombineUrl(RazorConfig.Theme.BasePath, p.Layout);
            var html = await razor.RenderPartial(contextAccessor.HttpContext, p.ViewPath, null, new Dictionary<string, object> { { nameof(PageOptionsDto), p } });
            return new RenderedPageResultDto
            {
                TemplateContent = html,
                Sources = p.GetSourcesString(),
                ViewParams = p.ViewParams
            };
        }

        public async Task<List<PageConfigurationDto>> GetPagesByCategory(long id)
        {
            var opts = await data.GetPageOptionsByCategory(id, 9);
            List<PageConfigurationDto> lst = new List<PageConfigurationDto>();
            foreach (var page in opts)
            {
                PageOptionsDto p = Mapper.Map(page, new PageOptionsDto());
                p.Layout = Utils.CombineUrl(RazorConfig.Theme.BasePath, p.Layout);
                if (p.ViewPath.Contains("Dashboard"))
                {

                }
                var html = razor.RenderPartial(contextAccessor.HttpContext, p.ViewPath, null, new Dictionary<string, object> { { nameof(PageOptionsDto), p } });
                lst.Add(new PageConfigurationDto
                {
                    Sources = p.Sources,
                    ViewParams = p.ViewParams,
                    DefaultAccessibility = p.DefaultAccessibility,
                    Layout = p.Layout,
                    PageIdentifier = p.PageIdentifier
                });
            }
            return lst;
        }

        public virtual async Task<TemplateDataCollector> GetTemplateData(long id)
        {
            var basicData = await data.GetPageCategoryBasicData(id);

            string layout = null;
            if (basicData.Layout != null)
                layout = Utils.CombineUrl(RazorConfig.Theme.BasePath, "Layout", basicData.Layout + "Layout.cshtml");
            else if ((new string[] { "Edit", "List" }).Contains(basicData.BaseComponent))
                layout = Utils.CombineUrl(RazorConfig.Theme.BasePath, "Layout", basicData.BaseComponent + "Layout.cshtml");
            return razor.GetCollector(contextAccessor.HttpContext, basicData.ViewPath, layout);
        }

    }
}
