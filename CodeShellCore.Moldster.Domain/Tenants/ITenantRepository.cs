using CodeShellCore.Data;
using CodeShellCore.Moldster.Sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Tenants
{
    public interface ITenantRepository : IKeyRepository<Tenant, long>
    {
        SyncResult SyncTenants(long src, long tar);
    }
}
