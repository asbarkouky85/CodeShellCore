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
        TemplateDataCollector GetTemplateData(long id);
        RenderedPageResultDto GetPage(PageAcquisitorDTO pageAcquisitorDTO);
        RenderedPageResultDto GetPageById(long id);
        Task<RenderedPageResultDto> GetPageCategoryById(long id);
        Task<List<PageConfigurationDto>> GetPagesByCategory(long id);
        string GetMainComponent(string baseComponent);
        string GetGuide(string moduleCode);
    }
}
