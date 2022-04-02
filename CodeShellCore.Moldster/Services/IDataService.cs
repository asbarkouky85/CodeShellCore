using CodeShellCore.Data.Helpers;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Services;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Services
{
    public interface IDataService : IServiceBase
    {

        PageRenderDTO[] GetDomainPagesForRendering(string mod, string domain, bool recursive = true);
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
