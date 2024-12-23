using CodeShellCore.Caching;
using CodeShellCore.MultiTenant;
using CodeShellCore.Security.Authentication;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Security.Authorization
{
    public class DbUserDataService : UserDataService, IUserDataService
    {
        private readonly ISecurityUnit unit;

        public DbUserDataService(ISecurityUnit unit, ICacheProvider cache, CurrentTenant tenant) : base(cache, tenant)
        {
            this.unit = unit;
        }

        protected override async Task<IUser> GetUserFromDataSource(string c)
        {
            var u = await unit.UserRepository.GetByUserId(RemoveTenantFromKey(c).ToString());
            if (u != null && u is IEntityLinkedUser)
                ((IEntityLinkedUser)u).EntityLinks = await unit.UsersEntityLinkRepository.GetUserLinks(u.UserId);
            return u;
        }

        protected override async Task<List<RoleCacheItem>> GetRolesFromDataSource(IEnumerable<object> roles)
        {
            List<RoleCacheItem> res = new List<RoleCacheItem>();
            foreach (var role in roles)
            {
                var roleId = RemoveTenantFromKey(role);
                var roleResources = await unit.ResourceRepository.GetRoleResources(RemoveTenantFromKey(roleId));
                res.Add(new RoleCacheItem
                {
                    RoleId = roleId,
                    Actions = await unit.ResourceRepository.GetRoleResourceActions(roleId),
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
