using CodeShellCore.Data;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Resources;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.PageCategories
{
    public interface IPageCategoryRepository : IRepository<PageCategory>
    {
        IEnumerable<long> GetDomainTemplates(string domain, long tenantId);
        LoadResult<PageCategoryListDTO> GetUnderDomain(long domainId, LoadOptions opt);
        void Add(PageCategory cat, Domain dom, Resource res = null);
        IEnumerable<ModuleCategoryDTO> GetByMoldsterModule(string installPath);
    }
}
