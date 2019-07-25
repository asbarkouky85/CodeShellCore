using Asga.Security;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Web.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Asga.Auth.Security
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

        public override ISessionManager SessionManager { get { return _manager; } }

        protected override bool IsAuthorized(AuthorizationRequest<AuthorizationFilterContext> req)
        {
            var user = req.Context.HttpContext.RequestServices.GetCurrentUser() as UserDTO;
            if (user == null)
                return false;
            //used to check if user is activated or not
            var userStatus = _unit.AuthUserRepository.GetValue(user.Id, x => x.IsDeleted);
            if (userStatus)
                return false;
            if (user.Permissions.TryGetValue(req.Resource + "__" + req.Resource, out Permission perm))
            {
                var permissions = perm as AsgaPermission;
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
                }
            }

            return false;
            //return SessionManager.IsLoggedIn();
        }



    }
}
