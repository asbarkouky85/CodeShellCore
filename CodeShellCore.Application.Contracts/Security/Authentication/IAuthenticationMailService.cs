using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Security.Authentication
{
    public interface IAuthenticationMailService
    {
        Task<Result> SendResetEmail(ResetPasswordDTO newPassword);
    }
}
