using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MultiTenant
{
    public class NullTenantDataProvider : ITenantDataProvider
    {
        public void ClearCache()
        {
            
        }

        public string GenerateToken(long userId, long tenantId)
        {
            return "";
        }

        public Dictionary<long, string> GetContectionStringDictionary()
        {
            return new Dictionary<long, string>();
        }

        public string GetTenantCode(long tenantId)
        {
            return "";
        }

        public string GetTenantConnectionString(long tenantId)
        {
            return null;
        }

        public string GetTenantLogo(long tenantId)
        {
            return "";
        }

        public void LoadDictionaries()
        {
            
        }
    }
}
