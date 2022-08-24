using CodeShellCore.Data;
using CodeShellCore.Data.Recursion;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Domains
{
    public interface IDomainRepository : IRepository<Domain>, IRecursiveRepository<Domain>
    {
        Domain GetOrCreatePath(string dom);
        Domain GetOrCreatePath(string dom, ref List<Domain> doms);
        Domain GetDomainByPath(string domain);
        List<T> GetByTenantCodeForRouting<T>(string moduleCode, long? domId = null) where T : class;
        IEnumerable<T> GetParentModules<T>(long modId);
        IEnumerable<Domain> GetHavingPagesForTenant(long value);
        IEnumerable<Domain> GetHavingCategories();
    }
}
