using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security.Authentication
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; }
        public string VarificationCode { get; set; }
        public string NewPassword { get; set; }
    }
}
