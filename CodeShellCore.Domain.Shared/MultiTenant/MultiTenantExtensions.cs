using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MultiTenant
{
    public static class MultiTenantExtensions
    {
        public static void SetCurrentTenant(this IServiceProvider provider, long tenantId)
        {
            provider.GetRequiredService<CurrentTenant>().TenantId = tenantId;
        }

        public static void SetCurrentTenantVersion(this IServiceProvider provider, string version)
        {
            provider.GetRequiredService<CurrentTenant>().Version = version;
        }

    }
}
