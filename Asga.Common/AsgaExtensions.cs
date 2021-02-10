using Asga.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga
{
    public static class AsgaExtensions
    {
        public static void SetCurrentTenant(this IServiceProvider provider, long tenantId)
        {
            provider.GetRequiredService<CurrentTenant>().TenantId = tenantId;
        }

        public static long GetCurrentTenant(this IServiceProvider provider)
        {
            return provider.GetRequiredService<CurrentTenant>().TenantId;
        }

    }
}
