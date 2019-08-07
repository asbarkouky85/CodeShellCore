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
        public string Name { get; set; }
        public object UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public string Provider { get; set; }
        public IEnumerable Roles { get; set; }
        public string DeviceId { get; set; }
        public string TokenId { get; set; }

        public virtual bool IsValid(string provider)
        {
            return ExpireTime > DateTime.Now && (string.IsNullOrEmpty(Provider) || Provider.ToLower() == provider.ToLower());
        }
    }
}
