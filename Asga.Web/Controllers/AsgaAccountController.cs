using CodeShellCore.Security.Authentication;
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

        public AsgaAccountController(IAuthenticationService service)
        {
            this.service = service;
        }


        public virtual IActionResult Login([FromBody]LoginModel model)
        {
            return Respond(service.Login(model.UserName, model.Password));
        }
    }
}
