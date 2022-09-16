using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Tasks;
using System.Net;
using System;
using CodeShellCore.Text;
using System.Collections.Generic;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages;

namespace CodeShellCore.Moldster.Razor
{
    public class DefaultViewsService : HttpService, IViewsService
    {
        readonly IPathsService Paths;
        protected override string BaseUrl { get { return "/Views"; } }

        public DefaultViewsService(IPathsService paths)
        {
            Paths = paths;
        }

        public TemplateDataCollector GetTemplateData(long id)
        {
            string data = Get("GetTemplateData/" + id).Content.ReadAsStringAsync().GetTaskResult();
            return data.FromJson<TemplateDataCollector>() ?? new TemplateDataCollector { Controls = new List<ControlRenderDto>() };
        }

        public RenderedPageResult GetPage(PageAcquisitorDTO pageAcquisitorDTO)
        {
            var s = Get("GetPage", pageAcquisitorDTO);
            return new RenderedPageResult { TemplateContent = s.Content.ReadAsStringAsync().GetTaskResult() };
        }

        public string GetPage(string viewPath)
        {
            var s = Get("GetPage/?ViewPath=" + viewPath);
            return s.Content.ReadAsStringAsync().GetTaskResult();
        }

        public string GetMainComponent(string baseComponent)
        {
            var s = Get("GetMainComponent/?ViewPath=" + baseComponent);
            return s.Content.ReadAsStringAsync().GetTaskResult();
        }

        public string GetGuide(string moduleCode)
        {
            var s = Get("GetGuide/" + moduleCode);
            return s.Content.ReadAsStringAsync().GetTaskResult();
        }

        public RenderedPageResult GetPageById(long id)
        {
            var s = Get("GetPageById/" + id);
            return new RenderedPageResult { TemplateContent = s.Content.ReadAsStringAsync().GetTaskResult() };
        }

    }
}
