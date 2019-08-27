using CodeShellCore.Data;
using CodeShellCore.Moldster.Db.Dto;
using CodeShellCore.Services.Recursive;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Data
{
    public interface IDomainRepository : IRepository<Domain>, IRecursiveRepository<Domain>
    {
        Domain GetOrCreatePath(string dom);
        Domain GetDomainByPath(string domain);
        List<DomainWithPagesDTO> GetByTenantCodeForRouting(string moduleCode, long? domId = null);
        IEnumerable<DomainWithPagesDTO> GetParentModules(long modId);
    }
}
