using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Security
{
    public interface IAccountController
    {
        LoginResult Login(LoginModel model);
        object GetUserData();
        LoginResult RefreshToken(RefreshTokenDTO refresh);
    }
}
