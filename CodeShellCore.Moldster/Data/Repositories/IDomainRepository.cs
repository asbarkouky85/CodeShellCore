using CodeShellCore.Data;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Data.Recursion;
using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Moldster;

namespace CodeShellCore.Moldster.Data.Repositories
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
