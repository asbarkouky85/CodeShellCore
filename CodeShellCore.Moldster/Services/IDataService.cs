using CodeShellCore.Data.Helpers;
using CodeShellCore.Moldster.Db.Dto;
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

        PageRenderDTO[] GetDomainPagesForRendering(string mod, string domain);
        string[] GetTemplatePaths(string modCode, string domain = null);
        string[] GetAppCodes(bool? active = null);
        IEnumerable<DomainRecursive> GetModuleDomains(string modCode);
        PageOptions GetPageOptions(string moduleCode, string viewPath);
        TenantPageGuideDTO GetAppGuide(long id);
        PageOptions GetPageOptionsById(long id);
        string GetAppStyle(string modCode);
        string GetAppVersion(string code);
        SubmitResult SetAppVersion(string code, string version);
    }
}
