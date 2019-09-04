using CodeShellCore.Caching;
using CodeShellCore.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security.Authorization
{
    public abstract class UserDataServiceBase : ServiceBase
    {
        protected readonly ICacheProvider cache;

        public UserDataServiceBase(ICacheProvider cache)
        {
            this.cache = cache;
        }

      
        protected abstract RoleCacheItem GetRoleFromDataSource(object id);
        protected abstract IUser GetUserFromDataSource(object c);
        public Dictionary<string, Permission> GetRolesPermissions(IEnumerable lst)
        {
            List<ResourceActionV> ras = new List<ResourceActionV>();
            List<ResourceV> rs = new List<ResourceV>();

            foreach (object role in lst)
            {
                var c = cache.Get<RoleCacheItem>(role);
                if (c == null)
                {
                    c = GetRoleFromDataSource(role);
                    cache.Store(role, c);
                }
                ras.AddRange(c.Actions);
                rs.AddRange(c.Resources);
            }
            return AccessibilityPermissions.GetDictionary(rs, ras);
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
            AppendPermissions(u);

            return u;
        }

        public void Save(object userId, IUser user)
        {
            cache.Store(userId, user);
        }

        public virtual void ClearUserData(object id)
        {
            cache.Remove<IUser>(id);
        }

    }
}
