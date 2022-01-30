using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security.Authentication
{
    public class ClientJwt
    {
        public string Secret { get; set; }
        public string ClientId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public string Provider { get; set; }
        public string TenantId { get; set; }
    }
}
