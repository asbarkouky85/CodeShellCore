using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Auth.Dto
{
    public class UserPartiesChanged
    {
        public long UserId { get; set; }
        public long[] Parties { get; set; }
        public long TenantId { get; set; }
    }
}
