using CodeShellCore.Data;
using CodeShellCore.Data.Events;
using CodeShellCore.Files;
using CodeShellCore.MultiTenant;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Extensions
{
    public static class DomainExtenasions
    {
        public static void SetCurrentTenant(this IServiceProvider provider, long tenantId)
        {
            provider.GetRequiredService<CurrentTenant>().TenantId = tenantId;
        }

        public static void SaveFile(this IUnitOfWork unit, ITempFileData data, string folder = null)
        {
            if (data?.FileTempPath != null)
                unit.AddDistributedEvent(new TempFileConfirmed(data, unit.CurrentTenant?.TenantId, folder));
        }
    }
}
