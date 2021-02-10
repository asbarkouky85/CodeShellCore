using CodeShellCore.Security.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Security
{
    public class AsgaJWTData :JWTData
    {
        public long TenantId { get; set; }
        public string TenantCode { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
