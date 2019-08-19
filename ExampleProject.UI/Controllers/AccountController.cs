using Asga.Web.Controllers;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleProject.UI.Controllers
{
    public class AccountController : AsgaAccountController
    {

        public AccountController(IAuthenticationService service, ISessionManager manager) : base(service, manager)
        {
        }
    }
}
