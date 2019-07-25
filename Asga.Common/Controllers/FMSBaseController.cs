using CodeShellCore.Web.Controllers;
using Asga.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Controllers
{
    public class AsgaBaseController : BaseApiController
    {
        private UserDTO _user;
        protected UserDTO AsgaUser
        {
            get
            {
                if (_user == null)
                    _user = (UserDTO)CodeShellCore.Shell.User;
                return _user;
            }
        }
    }
}
