using CodeShellCore.Data;
using CodeShellCore.Linq;
using CodeShellCore.Moldster.Domains;
using CodeShellCore.Moldster.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.PageCategories
{
    public interface IPageCategoryRepository : IRepository<PageCategory>
    {
        Task<IEnumerable<long>> GetDomainTemplates(string domain, long tenantId);
        Task<PagedResult<T>> GetUnderDomain<T>(long domainId, PagedListRequest opt) where T : class;
        void Add(PageCategory cat, Domain dom, Resource res = null);
        Task<IEnumerable<T>> GetByMoldsterModule<T>(string installPath);
    }
}
