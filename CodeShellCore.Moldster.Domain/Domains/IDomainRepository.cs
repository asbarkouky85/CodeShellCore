using CodeShellCore.Data;
using CodeShellCore.Data.Recursion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Domains
{
    public interface IDomainRepository : IRepository<Domain>, IRecursiveRepository<Domain, DefaultRecursionModel>
    {
        Task<Domain> GetOrCreatePath(string dom);
        Task<Domain> GetOrCreatePath(string dom, List<Domain> doms);
        Task<Domain> GetDomainByPath(string domain);
        Task<List<T>> GetByTenantCodeForRouting<T>(string moduleCode, long? domId = null) where T : class;
        Task<List<T>> GetParentModules<T>(long modId);
        Task<List<Domain>> GetHavingPagesForTenant(long value);
        Task<List<Domain>> GetHavingCategories();
    }
}
