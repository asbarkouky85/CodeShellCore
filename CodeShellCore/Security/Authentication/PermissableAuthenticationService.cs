using CodeShellCore.Security.Authorization;

namespace CodeShellCore.Security.Authentication
{
    public class PermissableAuthenticationService : AuthenticationService
    {
        private readonly IUserDataService usrData;

        public PermissableAuthenticationService(ISecurityUnit unit, IUserDataService usrData) : base(unit)
        {
            this.usrData = usrData;
        }

        protected override void OnLoginAttempt(IUser user)
        {
            if (user != null)
            {
                if (user is IAuthorizableUser)
                {
                    var authUser = (IAuthorizableUser)user;
                    authUser.Permissions = usrData.GetRolesPermissions(authUser.Roles);
                }
                if (user is IEntityLinkedUser)
                    ((IEntityLinkedUser)user).EntityLinks = SecurityUnit.UsersEntityLinkRepository.GetUserLinks(user.UserId);
            }
        }
    }
}
