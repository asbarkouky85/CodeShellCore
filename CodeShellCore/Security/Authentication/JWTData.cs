using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Security.Authentication
{
    public class JWTData
    {
        public string UserId { get; set; }
        public string ClientId { get; set; }
        public string TenantId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public string Provider { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
        public string DeviceId { get; set; }
        public string TokenId { get; set; }

    }
}
