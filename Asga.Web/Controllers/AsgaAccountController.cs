using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Web.Controllers;
using CodeShellCore.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Web.Controllers
{
    [ApiAuthorize(AllowAnonymous = true)]
    public class AsgaAccountController : BaseApiController
    {
        private readonly IAuthenticationService service;
        private readonly ISessionManager manager;

        IUserDataService userData => GetService<IUserDataService>();

        public AsgaAccountController(IAuthenticationService service, ISessionManager manager)
        {
            this.service = service;
            this.manager = manager;
        }


        public virtual IActionResult Login([FromBody]LoginModel model)
        {
            return Respond(service.Login(model.UserName, model.Password));
        }

        [ApiAuthorize(AllowAll = true)]
        public virtual IActionResult GetUserData()
        {
            var res = userData.GetUserData(manager.GetCurrentUserId());
            return Respond(res);
        }
    }
}
