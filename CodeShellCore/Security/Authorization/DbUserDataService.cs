using CodeShellCore.Caching;
using System.Collections.Generic;
using System.Linq;

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
                var roleResources = unit.ResourceRepository.GetRoleResources(role);
                res.Add(new RoleCacheItem
                {
                    RoleId = role,
                    Actions = unit.ResourceRepository.GetRoleResourceActions(role),
                    Resources = CompressResourceData(roleResources),
                    Collections = CompressCollectionIds(roleResources)
                });
            }
            return res;
        }

        protected virtual Dictionary<string, string> CompressCollectionIds(IEnumerable<ResourceV> res)
        {
            if (res.Any())
            {
                return res.Where(d => d.CollectionId != null).ToDictionary(d => d.Id, d => d.CollectionId);
            }
            return null;
        }

        protected virtual Dictionary<string, int> CompressResourceData(IEnumerable<ResourceV> items)
        {
            Dictionary<string, int> ret = new Dictionary<string, int>();
            foreach (var d in items)
            {
                int perm = 0;
                ret.TryGetValue(d.Id, out perm);
                perm = Permission.Combine(perm, d);
                ret[d.Id] = perm;
            }
            return ret;
        }
    }
}
