using CodeShellCore.Moldster.Definitions;
using CodeShellCore.Moldster.Razor;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Services
{
    public interface IDataService : IServiceBase
    {

        string[] GetDomainPages(string mod, string domain);
        string[] GetTemplatePaths(string modCode, string domain = null);
        string[] GetModuleNames(bool? active = null);
        string[] GetModuleDomains(string modCode);
        PageOptions GetPageOptions(string moduleCode, string viewPath);
    }
}
