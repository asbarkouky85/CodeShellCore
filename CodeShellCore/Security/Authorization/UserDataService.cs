﻿using CodeShellCore.Caching;
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

        public virtual Dictionary<string, DataAccessPermission> GetRolesPermissions(IEnumerable<string> lst)
        {
            var ret = new Dictionary<string, DataAccessPermission>();
            List<object> unCached = new List<object>();

            foreach (string role in lst)
            {
                var roleFromCache = GetRoleFromCache(role);
                if (roleFromCache != null)
                {
                    ret = Append(ret, roleFromCache);
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
                    ret = Append(ret, fromSrc);
                }
            }

            return ret;
        }

        protected virtual Dictionary<string, DataAccessPermission> Append(Dictionary<string, DataAccessPermission> permissions, RoleCacheItem roleItem)
        {
            foreach (var r in roleItem.Resources)
            {
                if (!permissions.TryGetValue(r.Key, out DataAccessPermission perm))
                    perm = new DataAccessPermission(0);
                
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
            cache.Store(item.RoleId.ToString(), item);
        }

        protected virtual RoleCacheItem GetRoleFromCache(string role)
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
