using CodeShellCore.Data.Helpers;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.PageCategories;
using CodeShellCore.Moldster.Pages;
using CodeShellCore.Moldster.Tenants;
using CodeShellCore.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Services
{
    public interface IDataService : IServiceBase
    {

        Task<PageRenderDTO[]> GetDomainPagesForRendering(string mod, string domain, bool recursive = true);
        Task<string[]> GetTemplatePaths(string modCode, string domain = null);
        Task<string[]> GetAppCodes(bool? active = null);
        Task<IEnumerable<DomainRecursive>> GetModuleDomains(string modCode);
        Task<PageOptionsDto> GetPageOptions(string moduleCode, string viewPath);
        Task<TenantPageGuideDTO> GetAppGuide(long id);
        Task<PageOptionsDto> GetPageOptionsById(long id);
        Task<PageOptionsDto> GetCategoryPageOptions(long pageCategoryId);
        Task<PageCategoryBasicDataDto> GetPageCategoryBasicData(long pageCategoryId);
        Task<string> GetAppStyle(string modCode);
        Task<string> GetAppVersion(string code);
        Task<SubmitResult> SetAppVersion(string code, string version);
        Task<IEnumerable<PageOptionsDto>> GetPageOptionsByCategory(long categoryId, long tenantId);
        Task<long> GetTenantIdByCode(string moduleCode);
    }
}
