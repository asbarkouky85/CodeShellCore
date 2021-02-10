using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Sessions;
using Microsoft.AspNetCore.Mvc;

namespace Configurator.UI.Controllers
{
    public class AccountController : CodeShellCore.Web.Controllers.AccountControllerBase
    {
        private readonly IUserAccessor acc;

        public AccountController(IAuthenticationService service, IUserAccessor acc, ISessionManager manager) : base(service, manager)
        {
            this.acc = acc;
        }

        public IActionResult GetUserData()
        {
            return Respond(acc.User);
        }
    }
}
