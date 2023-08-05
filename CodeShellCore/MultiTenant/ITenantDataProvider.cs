using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MultiTenant
{
    public interface ITenantDataProvider
    {
        string GetTenantConnectionString(long tenantId);
        void LoadDictionaries();
        string GetTenantCode(long tenantId);
        string GetTenantLogo(long tenantId);
        string GenerateToken(long userId, long tenantId);
        Dictionary<long, string> GetContectionStringDictionary();
        void ClearCache();
        
    }
}
