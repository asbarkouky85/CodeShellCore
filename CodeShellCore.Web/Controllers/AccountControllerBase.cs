using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Controllers
{
    public class AccountControllerBase : BaseApiController
    {
        readonly IAuthenticationService _service;
        readonly ISessionManager _manager;
        public AccountControllerBase(IAuthenticationService service, ISessionManager manger)
        {
            _service = service;
            _manager = manger;
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginModel model)
        {
            return Respond(_service.Login(model.UserName, model.Password));
        }

        public IActionResult RefreshToken([FromBody]RefreshTokenDTO refresh)
        {
            var uid = _manager.CheckRefreshTokenWEB(refresh.Token);
            LoginResult res = new LoginResult(false, "InvalidToken");
            if (uid != null)
            {
                res = _service.LoginById(uid);
            }
            return Respond(res);
        }
    }
}
