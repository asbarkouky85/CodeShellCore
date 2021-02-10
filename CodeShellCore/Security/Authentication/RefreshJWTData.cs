using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security.Authentication
{
    public class RefreshJWTData
    {
        public string DeviceId { get; set; }
        public string TokenId { get; set; }
    }
}
