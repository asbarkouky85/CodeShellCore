using CodeShellCore.MultiTenant;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.EntityFramework.Migrations
{

    public interface IMultiTenantDbMigrationService : IDbMigrationService
    {
        void SetCurrentTenant(CurrentTenant tenant);

    }
}
