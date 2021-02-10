using CodeShellCore.Http;
using CodeShellCore.Moldster.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Services
{
    public interface IViewsService
    {
        string GetPage(PageAcquisitorDTO pageAcquisitorDTO);
        string GetPageById(long id);
        string GetMainComponent(string baseComponent);
        string GetGuide(string moduleCode);
        bool CheckServer(out HttpResult res);
    }
}
