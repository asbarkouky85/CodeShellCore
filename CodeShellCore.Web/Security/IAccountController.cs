using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Security
{
    public interface IAccountController
    {
        Task<LoginResult> Login(LoginModel model);
        Task<object> GetUserData();
        Task<LoginResult> RefreshToken(RefreshTokenDTO refresh);
    }
}
