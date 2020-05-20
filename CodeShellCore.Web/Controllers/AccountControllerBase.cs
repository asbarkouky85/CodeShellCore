using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Web.Filters;
using CodeShellCore.Web.Security;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web.Controllers
{
    [ApiAuthorize(AllowAnonymous =true)]
    public class AccountControllerBase : BaseApiController, IAccountController
    {
        readonly IAuthenticationService _service;
        readonly ISessionManager _manager;
        IUserDataService userData => GetService<IUserDataService>();

        public AccountControllerBase(IAuthenticationService service, ISessionManager manger)
        {
            _service = service;
            _manager = manger;
        }

        [HttpPost]
        [ApiAuthorize(AllowAnonymous =true)]
        public virtual IActionResult Login([FromBody]LoginModel model)
        {
            return Respond(_service.Login(model.UserName, model.Password));
        }

        [ApiAuthorize(AllowAnonymous = true)]
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

        [ApiAuthorize(AllowAll = true, AllowAnonymous = false)]
        public virtual IActionResult GetUserData()
        {
            var res = userData.GetUserDataForUI(_manager.GetCurrentUserId());
            return Respond(res);
        }
    }
}
