using CodeShellCore.Caching;
using CodeShellCore.Data;
using CodeShellCore.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security.Authorization
{
    public class UserDataService : ServiceBase, IUserDataService
    {
        private readonly ISecurityUnit unit;
        private readonly ICacheProvider cache;

        public UserDataService(ISecurityUnit unit, ICacheProvider cache)
        {
            this.unit = unit;
            this.cache = cache;
        }

        public void ClearUserData(object id)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, Permission> GetRolesPermissions(IEnumerable lst)
        {
            List<ResourceActionV> ras = new List<ResourceActionV>();
            List<ResourceV> rs = new List<ResourceV>();

            foreach (object role in lst)
            {
                var c = cache.Get<RoleCacheItem>(role);
                if (c == null)
                {
                    c = new RoleCacheItem
                    {
                        RoleId = role,
                        Actions = unit.ResourceRepository.GetRoleResourceActions(role),
                        Resources = unit.ResourceRepository.GetRoleResources(role)
                    };
                    cache.Store(role, c);
                }
                ras.AddRange(c.Actions);
                rs.AddRange(c.Resources);
            }
            return AccessibilityPermissions.GetDictionary(rs, ras);
        }

        protected virtual IUser GetUserFromDataSource(object c)
        {
            IUser u = unit.UserRepository.GetByUserId(c);
            AppendPermissions(u);
            return u;
        }

        protected virtual void AppendPermissions(IUser user)
        {
            if (user != null && user is IAuthorizableUser)
            {
                var u = (IAuthorizableUser)user;
                u.Permissions = GetRolesPermissions(u.Roles);
            }
        }

        public IUser GetUserData(object userId)
        {

            if (userId == null || userId.Equals(0))
                return null;
            IUser u = cache.Get<IUser>(userId);

            if (u == null)
            {
                u = GetUserFromDataSource(userId);
                Save(userId, u);
            }
            return u;
        }

        public void Save(object userId, IUser user)
        {
            cache.Store(userId, user);
        }
    }
}
