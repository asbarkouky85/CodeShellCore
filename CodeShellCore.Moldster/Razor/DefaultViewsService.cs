using CodeShellCore.Helpers;
using CodeShellCore.Http;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<TemplateDataCollector> GetTemplateData(long id)
        {
            var data = await GetAsyncAs<TemplateDataCollector>("GetTemplateData/" + id);
            return data ?? new TemplateDataCollector { Controls = new List<ControlRenderDto>() };
        }

        public async Task<RenderedPageResultDto> GetPage(PageAcquisitorDTO pageAcquisitorDTO)
        {
            var s = await GetAsyncAsString("GetPage", pageAcquisitorDTO);
            return new RenderedPageResultDto { TemplateContent = s };
        }

        public async Task<string> GetPage(string viewPath)
        {
            return await GetAsyncAsString("GetPage/?ViewPath=" + viewPath);
        }

        public async Task<string> GetMainComponent(string baseComponent)
        {
            return await GetAsyncAsString("GetMainComponent/?ViewPath=" + baseComponent);
        }

        public Task<string> GetGuide(string moduleCode)
        {
            return GetAsyncAsString("GetGuide/" + moduleCode);
        }

        public async Task<RenderedPageResultDto> GetPageById(long id)
        {
            var s = await GetAsyncAsString("GetPageById/" + id);
            return new RenderedPageResultDto { TemplateContent = s };
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
