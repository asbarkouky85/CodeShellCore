using CodeShellCore;
using CodeShellCore.Text;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Web.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Sessions;

namespace Asga.Security
{
    public class MicroServiceAuthorizationService : AccessControlAuthorizationService, IAccessControlAuthorizationService
    {
        public MicroServiceAuthorizationService(IAuthenticationService auth, ISessionManager manager) : base(auth, manager)
        {
        }

        UserDTO User { get { return (UserDTO)SessionManager.GetUserData(); } }

        protected override bool IsAuthorized(AuthorizationRequest<AuthorizationFilterContext> req)
        {
            return SessionManager.IsLoggedIn();
        }

        

    }
}
