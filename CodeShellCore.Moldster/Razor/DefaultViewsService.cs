using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Tasks;
using System.Net;
using System;
using CodeShellCore.Text;
using System.Collections.Generic;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages;
using System.Threading.Tasks;
using CodeShellCore.Moldster.Views;

namespace CodeShellCore.Moldster.Razor
{
    public class DefaultViewsService : HttpService, IViewsService
    {
        readonly IPathsService Paths;
        private string _baseUrl;
        protected override string BaseUrl => _baseUrl;

        public DefaultViewsService(IPathsService paths)
        {
            Paths = paths;
            _baseUrl = Utils.CombineUrl(paths.ConfigUrl, "api/Views");
        }

        public TemplateDataCollector GetTemplateData(long id)
        {
            string data = Get("GetTemplateData/" + id).Content.ReadAsStringAsync().GetTaskResult();
            return data.FromJson<TemplateDataCollector>() ?? new TemplateDataCollector { Controls = new List<ControlRenderDto>() };
        }

        public RenderedPageResultDto GetPage(PageAcquisitorDTO pageAcquisitorDTO)
        {
            var s = Get("GetPage", pageAcquisitorDTO);
            return new RenderedPageResultDto { TemplateContent = s.Content.ReadAsStringAsync().GetTaskResult() };
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

        public RenderedPageResultDto GetPageById(long id)
        {
            var s = Get("GetPageById/" + id);
            return new RenderedPageResultDto { TemplateContent = s.Content.ReadAsStringAsync().GetTaskResult() };
        }

        public async Task<RenderedPageResultDto> GetPageCategoryById(long id)
        {
            return await GetAsyncAs<RenderedPageResultDto>("GetPageCategoryById/" + id);
        }

        public async Task<List<PageConfigurationDto>> GetPagesByCategory(long id)
        {
            return await GetAsyncAs<List<PageConfigurationDto>>("GetPagesByCategory/" + id);
        }
    }
}
