using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Controllers
{
    public class AccountControllerBase : BaseApiController
    {
        readonly AuthorizationService Service;
        public AccountControllerBase(AuthorizationService service)
        {
            Service = service;
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginModel model)
        {
            return Respond(Service.Login(model));
        }

        public IActionResult RefreshToken([FromBody]RefreshTokenDTO refresh)
        {
            var uid = Service.SessionManager.CheckRefreshTokenWEB(refresh.Token);
            LoginResult res = new LoginResult(false, "InvalidToken");
            if (uid != null)
            {
                res = Service.AuthenticationService.LoginById(uid);
            }
            return Respond(res);
        }
    }
}
