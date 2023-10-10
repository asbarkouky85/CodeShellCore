using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MultiTenant
{
    public interface ITenantDataProvider
    {
        string GetTenantConnectionString(long tenantId);
        Dictionary<long, string> GetContectionStringDictionary();
        void LoadDictionaries();
        void ClearCache();
        string GetTenantCode(long tenantId);
        string GetTenantLogo(long tenantId);
        string GenerateToken(long userId, long tenantId);
        
    }
}
