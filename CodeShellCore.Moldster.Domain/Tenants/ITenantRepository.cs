using CodeShellCore.Data;
using CodeShellCore.Moldster.Sql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Tenants
{
    public interface ITenantRepository : IKeyRepository<Tenant, long>
    {
        Task<SyncResult> SyncTenants(long src, long tar);
    }
}
