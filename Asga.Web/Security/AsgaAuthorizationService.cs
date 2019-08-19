using Asga.Auth;
using Asga.Security;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Web.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Asga.Web.Security
{
    public class AsgaAuthorizationService : AccessControlAuthorizationService, IAccessControlAuthorizationService
    {
        private readonly ISessionManager _manager;
        private readonly AuthUnit _unit;
        private UserDTO User;

        public AsgaAuthorizationService(IUserAccessor acc, ISessionManager manager, AuthUnit unit, IAuthenticationService auth) : base(auth, manager)
        {
            _manager = manager;
            _unit = unit;
            User = acc.User as UserDTO;
        }

        protected override bool IsAuthorized(AuthorizationRequest<AuthorizationFilterContext> req)
        {
            
            if (User.Permissions.TryGetValue(req.Resource, out Permission perm))
            {
                var permissions = perm as AccessibilityPermissions;
                switch (req.Action)
                {
                    case "CanViewDetails":
                        return permissions.Details;
                    case "CanInsert":
                        return permissions.Insert;
                    case "CanUpdate":
                        return permissions.Update;
                    case "CanViewDelete":
                        return permissions.Delete;
                    case "Get":
                        return permissions != null;
                }
            }

            return false;
            //return SessionManager.IsLoggedIn();
        }



    }
}
