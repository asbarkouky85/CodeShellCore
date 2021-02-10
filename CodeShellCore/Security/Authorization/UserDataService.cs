using CodeShellCore.Caching;
using CodeShellCore.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeShellCore.Security.Authorization
{
    public class UserDataService : ServiceBase, IUserDataService
    {
        protected readonly ICacheProvider cache;

        public UserDataService(ICacheProvider cache)
        {
            this.cache = cache;
        }


        protected virtual List<RoleCacheItem> GetRolesFromDataSource(IEnumerable<object> id)
        {
            return new List<RoleCacheItem>();
        }
        protected virtual IUser GetUserFromDataSource(object c)
        {
            return null;
        }
        public Dictionary<string, Permission> GetRolesPermissions(IEnumerable<object> lst)
        {

            List<ResourceActionV> ras = new List<ResourceActionV>();
            List<ResourceV> rs = new List<ResourceV>();
            List<object> unCached = new List<object>();
            foreach (object role in lst)
            {
                var c = GetRoleFromCache(role);
                if (c != null)
                {
                    ras.AddRange(c.Actions);
                    rs.AddRange(c.Resources);
                }
                else
                {
                    unCached.Add(role);
                }
            }

            if (unCached.Any())
            {
                var find = GetRolesFromDataSource(unCached);
                foreach (var f in find)
                {
                    SaveRoleInCache(f);
                    ras.AddRange(f.Actions);
                    rs.AddRange(f.Resources);
                }
            }


            return AccessibilityPermissions.GetDictionary(rs, ras);
        }

        protected virtual void SaveRoleInCache(RoleCacheItem item)
        {
            cache.Store(item.RoleId, item);
        }

        protected virtual RoleCacheItem GetRoleFromCache(object role)
        {
            return cache.Get<RoleCacheItem>(role);
        }

        protected virtual void AppendPermissions(IUser user)
        {
            if (user != null && user is IAuthorizableUser)
            {
                var u = (IAuthorizableUser)user;
                if (u.Roles != null)
                    u.Permissions = GetRolesPermissions(u.Roles);
            }
        }

        public IUser GetUserData(object userId)
        {
            if (userId == null || userId.Equals(0))
                return null;
            IUser u = GetUserFromCache(userId);
            if (u == null)
            {
                u = GetUserFromDataSource(userId);
                Save(userId, u);
            }
            AppendPermissions(u);

            return u;
        }

        protected virtual IUser GetUserFromCache(object userId)
        {
            return cache.Get<IUser>(userId);
        }

        public virtual void Save(object userId, IUser user)
        {
            cache.Store(userId, user);
        }

        public virtual void ClearUserData(object id)
        {
            cache.Remove<IUser>(id);
        }

    }
}
