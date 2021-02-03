using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security.Authentication
{
    public interface IAuthenticationMailService
    {
        Result SendResetEmail(ResetPasswordDTO newPassword);
    }
}
