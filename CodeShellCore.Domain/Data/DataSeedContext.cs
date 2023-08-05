using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data
{
    public class DataSeedContext
    {
        public long? TenantId { get; private set; }
        public string Code { get; private set; }

        public DataSeedContext(long? tenantId = null, string code = null)
        {
            TenantId = tenantId;
            Code = code;
        }
    }
}
