using CodeShellCore;
using CodeShellCore.Text;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Web.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Sessions;
using Asga.Security;

namespace Asga.Web.Security
{
    public class MicroServiceAuthorizationService : AccessControlAuthorizationService, IAccessControlAuthorizationService
    {
        public MicroServiceAuthorizationService(IAuthenticationService auth, ISessionManager manager) : base(auth, manager)
        {
        }

        protected override bool IsAuthorized(AuthorizationRequest<AuthorizationFilterContext> req)
        {
            return SessionManager.IsLoggedIn();
        }

    }
}
