using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security.Authentication
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
        public string NewPassword { get; set; }
        public string ServerUrl { get; set; }
        public string UserFullName { get; set; }
        public string LogonName { get; set; }
    }
}
