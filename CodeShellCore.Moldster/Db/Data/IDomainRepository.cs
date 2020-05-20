using CodeShellCore.Data;
using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Data.Recursion;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Data
{
    public interface IDomainRepository : IRepository<Domain>, IRecursiveRepository<Domain>
    {
        Domain GetOrCreatePath(string dom);
        Domain GetOrCreatePath(string dom, ref List<Domain> doms);
        Domain GetDomainByPath(string domain);
        List<DomainWithPagesDTO> GetByTenantCodeForRouting(string moduleCode, long? domId = null);
        IEnumerable<DomainWithPagesDTO> GetParentModules(long modId);
    }
}
