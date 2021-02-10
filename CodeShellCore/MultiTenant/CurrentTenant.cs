using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.MultiTenant
{
    public class CurrentTenant
    {
        ITenantDataProvider _provider;
        public CurrentTenant(ITenantDataProvider prov)
        {
            _provider = prov;
        }
        public long TenantId { get; set; }
        public string Code { get { return _provider.GetTenantCode(TenantId); } }
        public string Version { get; set; }

        public string GetConnectionString()
        {
            return _provider.GetTenantConnectionString(TenantId);
        }

        public string GenerateToken(object userId)
        {
            return _provider.GenerateToken((long)userId, TenantId);
        }

        public bool VersionIsOrAfter(string ver)
        {
            if (string.IsNullOrEmpty(Version))
                return false;

            var parts1 = Version.Split('.');
            var parts2 = ver.Split('.');
            for (var i = 0; i < parts1.Length; i++)
            {
                if (i >= parts2.Length)
                    break;
                var p1 = int.Parse(parts1[i]);
                var p2 = int.Parse(parts2[i]);
                if (p1 > p2)
                    return true;
                else if (p1 < p2)
                    return false;
            }
            return true;
        }
    }
}
