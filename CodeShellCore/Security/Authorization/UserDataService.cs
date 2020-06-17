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
        protected virtual IUser GetUserFromDataSource(string c)
        {
            return null;
        }
        public virtual Dictionary<string, DataAccessPermission> GetRolesPermissions(IEnumerable<object> lst)
        {

            List<ResourceActionV> ras = new List<ResourceActionV>();
            Dictionary<string, Permission> rs = new Dictionary<string, Permission>();
            var ret = new Dictionary<string, DataAccessPermission>();
            List<object> unCached = new List<object>();

            foreach (object role in lst)
            {
                var roleFromCache = GetRoleFromCache(role);
                if (roleFromCache != null)
                {
                    rs = Append(rs, roleFromCache);
                }
                else
                {
                    unCached.Add(role);
                }
            }

            if (unCached.Any())
            {
                var find = GetRolesFromDataSource(unCached);
                foreach (var fromSrc in find)
                {
                    SaveRoleInCache(fromSrc);
                    rs = Append(rs, fromSrc);
                }
            }
            
            foreach (var per in rs)
            {
                ret[per.Key] = per.Value.ToDataPermission();
            }
            return ret;
        }

        protected virtual Dictionary<string, Permission> Append(Dictionary<string, Permission> permissions, RoleCacheItem roleItem)
        {
            foreach (var r in roleItem.Resources)
            {
                if (!permissions.TryGetValue(r.Key, out Permission perm))
                    perm = new Permission(0);
                
                if (roleItem.Collections!=null && roleItem.Collections.TryGetValue(r.Key, out string coll))
                    perm.CollectionId = coll;

                perm.Append(r.Value);
                var acs = roleItem.Actions.Where(d => d.Id == r.Key).Select(d => d.Action);
                foreach (var a in acs)
                    perm.Append(a);
                permissions[r.Key] = perm;
            }
            return permissions;
        }

        public virtual void SaveRoleInCache(RoleCacheItem item)
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

        public virtual IUser GetUserData(string userId)
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

        public virtual IUser GetUserDataForUI(string userId)
        {
            return GetUserData(userId);
        }

        protected virtual IUser GetUserFromCache(string userId)
        {
            return cache.Get<IUser>(userId);
        }

        public virtual void Save(string userId, IUser user)
        {
            cache.Store(userId, user);
        }

        public virtual void ClearUserData(string id)
        {
            cache.Remove<IUser>(id);
        }


    }
}
