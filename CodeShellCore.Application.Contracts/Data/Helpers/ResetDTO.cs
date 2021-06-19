using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data.Helpers
{
    public class ResetDTO
    {
        public string TenantCode { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long UserId { get; set; }
        public string Token { get; set; }
    }
}
