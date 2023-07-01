using CodeShellCore.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Security
{
    public interface IAccountController
    {
        IActionResult Login([FromBody]LoginModel model);
        IActionResult GetUserData();
        IActionResult RefreshToken([FromBody]RefreshTokenDTO refresh);
    }
}
