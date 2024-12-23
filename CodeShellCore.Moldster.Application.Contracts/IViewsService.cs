using CodeShellCore.Http;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster
{
    public interface IViewsService
    {
        Task<TemplateDataCollector> GetTemplateData(long id);
        Task<RenderedPageResultDto> GetPage(PageAcquisitorDTO pageAcquisitorDTO);
        Task<RenderedPageResultDto> GetPageById(long id);
        Task<RenderedPageResultDto> GetPageCategoryById(long id);
        Task<List<PageConfigurationDto>> GetPagesByCategory(long id);
        Task<string> GetMainComponent(string baseComponent);
    }
}
