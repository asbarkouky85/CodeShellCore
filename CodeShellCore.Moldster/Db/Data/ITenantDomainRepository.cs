using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Data
{
    public interface ITenantDomainRepository : IRepository<TenantDomain>
    {
        TenantDomain GetTenantDomain(long domainId, string tenantCode);
    }
}
