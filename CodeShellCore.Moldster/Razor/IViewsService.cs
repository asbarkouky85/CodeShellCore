using CodeShellCore.Http;
using CodeShellCore.Moldster.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Razor
{
    public interface IViewsService
    {
        TemplateDataCollector GetTemplateData(long id);
        RenderedPageResult GetPage(PageAcquisitorDTO pageAcquisitorDTO);
        RenderedPageResult GetPageById(long id);
        string GetMainComponent(string baseComponent);
        string GetGuide(string moduleCode);
    }
}
