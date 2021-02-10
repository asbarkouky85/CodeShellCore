using CodeShellCore.Caching;
using System.Collections.Generic;

namespace CodeShellCore.Security.Authorization
{
    public class DbUserDataService : UserDataService, IUserDataService
    {
        private readonly ISecurityUnit unit;

        public DbUserDataService(ISecurityUnit unit, ICacheProvider cache) : base(cache)
        {
            this.unit = unit;
        }

        protected override IUser GetUserFromDataSource(object c)
        {
            var u = unit.UserRepository.GetByUserId(c);
            if (u != null && u is IEntityLinkedUser)
                ((IEntityLinkedUser)u).EntityLinks = unit.UsersEntityLinkRepository.GetUserLinks(u.UserId);
            return u;
        }

        protected override List<RoleCacheItem> GetRolesFromDataSource(IEnumerable<object> roles)
        {
            List<RoleCacheItem> res = new List<RoleCacheItem>();
            foreach (var role in roles)
            {
                res.Add(new RoleCacheItem
                {
                    RoleId = role,
                    Actions = unit.ResourceRepository.GetRoleResourceActions(role),
                    Resources = unit.ResourceRepository.GetRoleResources(role)
                });
            }
            return res;
        }
    }
}
