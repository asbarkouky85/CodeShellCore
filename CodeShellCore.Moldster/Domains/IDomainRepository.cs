using CodeShellCore.Data;
using CodeShellCore.Data.Recursion;
using CodeShellCore.Moldster.Domains.Dtos;
using System.Collections.Generic;

namespace CodeShellCore.Moldster.Domains
{
    public interface IDomainRepository : IRepository<Domain>, IRecursiveRepository<Domain>
    {
        Domain GetOrCreatePath(string dom);
        Domain GetOrCreatePath(string dom, ref List<Domain> doms);
        Domain GetDomainByPath(string domain);
        List<DomainWithPagesDTO> GetByTenantCodeForRouting(string moduleCode, long? domId = null);
        IEnumerable<DomainWithPagesDTO> GetParentModules(long modId);
        IEnumerable<Domain> GetHavingPagesForTenant(long value);
        IEnumerable<Domain> GetHavingCategories();
    }
}
