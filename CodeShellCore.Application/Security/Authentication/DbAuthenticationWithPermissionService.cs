using CodeShellCore.Security.Authorization;
using System.Threading.Tasks;

namespace CodeShellCore.Security.Authentication
{
    public class DbAuthenticationWithPermissionService : DbAuthenticationService
    {
        protected readonly IUserDataService UserData;

        public DbAuthenticationWithPermissionService(ISecurityUnit unit, IUserDataService data) : base(unit)
        {
            UserData = data;
        }

        protected override async Task OnLoginAttempt(IUser user)
        {
            if (user != null)
            {
                if (user is IAuthorizableUser)
                {
                    var authUser = (IAuthorizableUser)user;
                    authUser.Permissions = await UserData.GetRolesPermissions(authUser.Roles);
                }
                if (user is IEntityLinkedUser)
                    ((IEntityLinkedUser)user).EntityLinks = await SecurityUnit.UsersEntityLinkRepository.GetUserLinks(user.UserId);
            }
        }
    }
}
