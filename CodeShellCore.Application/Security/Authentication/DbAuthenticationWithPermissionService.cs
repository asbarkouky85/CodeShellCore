using CodeShellCore.Security.Authorization;

namespace CodeShellCore.Security.Authentication
{
    public class DbAuthenticationWithPermissionService : DbAuthenticationService
    {
        protected readonly IUserDataService UserData;

        public DbAuthenticationWithPermissionService(ISecurityUnit unit, IUserDataService data) : base(unit)
        {
            UserData = data;
        }

        protected override void OnLoginAttempt(IUser user)
        {
            if (user != null)
            {
                if (user is IAuthorizableUser)
                {
                    var authUser = (IAuthorizableUser)user;
                    authUser.Permissions = UserData.GetRolesPermissions(authUser.Roles);
                }
                if (user is IEntityLinkedUser)
                    ((IEntityLinkedUser)user).EntityLinks = SecurityUnit.UsersEntityLinkRepository.GetUserLinks(user.UserId);
            }
        }
    }
}
