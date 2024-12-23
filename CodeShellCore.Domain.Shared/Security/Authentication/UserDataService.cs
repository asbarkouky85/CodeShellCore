using CodeShellCore.Caching;
using CodeShellCore.Helpers;
using CodeShellCore.MultiTenant;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Security.Authentication
{
    public class UserDataService : ServiceBase, IUserDataService
    {
        protected ICacheProvider Cache { get; private set; }
        protected CurrentTenant CurrentTenant { get; private set; }

        public UserDataService(ICacheProvider cache, CurrentTenant currentTenant)
        {
            Cache = cache;
            CurrentTenant = currentTenant;
        }


        protected virtual Task<List<RoleCacheItem>> GetRolesFromDataSource(IEnumerable<object> id)
        {
            return Task.Run(() => { return new List<RoleCacheItem>(); });
        }

        protected virtual Task<IUser> GetUserFromDataSource(string c)
        {
            return Task.Run(() => { return (IUser)null; });
        }

        public virtual async Task<Dictionary<string, DataAccessPermission>> GetRolesPermissions(IEnumerable<string> lst)
        {
            var ret = new Dictionary<string, DataAccessPermission>();
            List<object> unCached = new List<object>();

            foreach (string role in lst)
            {
                var roleFromCache = await GetRoleFromCache(role);
                if (roleFromCache != null)
                {
                    ret = await AppendPermissions(ret, roleFromCache);
                }
                else
                {
                    unCached.Add(role);
                }
            }

            if (unCached.Any())
            {
                var find = await GetRolesFromDataSource(unCached);
                foreach (var fromSrc in find)
                {
                    await SaveRoleInCache(fromSrc);
                    ret = await AppendPermissions(ret, fromSrc);
                }
            }

            return ret;
        }

        protected virtual Task<Dictionary<string, DataAccessPermission>> AppendPermissions(Dictionary<string, DataAccessPermission> permissions, RoleCacheItem roleItem)
        {
            return Task.Run(() =>
            {

                foreach (var r in roleItem.Resources)
                {
                    if (!permissions.TryGetValue(r.Key, out DataAccessPermission perm))
                        perm = new DataAccessPermission(0);

                    if (roleItem.Collections != null && roleItem.Collections.TryGetValue(r.Key, out string coll))
                        perm.CollectionId = coll;

                    perm.Append(r.Value);
                    var acs = roleItem.Actions.Where(d => d.Id == r.Key).Select(d => d.Action);
                    foreach (var a in acs)
                        perm.Append(a);
                    permissions[r.Key] = perm;
                }
                return permissions;
            });
        }

        public virtual async Task SaveRoleInCache(RoleCacheItem item)
        {
            await Cache.Store(AddTenantToKey(item.RoleId.ToString()), item);
        }

        protected virtual object RemoveTenantFromKey(object key)
        {
            var split = Utils.SplitTenantEntity(key.ToString());
            return split.EntityId;
        }

        protected virtual string AddTenantToKey(string key)
        {
            if (CurrentTenant.TenantId != 0 && !key.StartsWith(CurrentTenant.TenantId + "_"))
            {
                return CurrentTenant.TenantId + "_" + key;
            }
            return key;
        }

        protected virtual Task<RoleCacheItem> GetRoleFromCache(string role)
        {
            return Cache.Get<RoleCacheItem>(AddTenantToKey(role));
        }

        protected virtual async Task AppendPermissions(IUser user)
        {
            if (user != null && user is IAuthorizableUser)
            {
                var u = (IAuthorizableUser)user;
                if (u.Roles != null)
                    u.Permissions = await GetRolesPermissions(u.Roles);
            }
        }

        public virtual async Task<IUser> GetUserData(string userId)
        {
            if (userId == null || userId.Equals(0))
                return null;
            IUser u = await GetUserFromCache(userId);
            if (u == null)
            {
                u = await GetUserFromDataSource(userId);
                await Save(userId, u);
            }
            await AppendPermissions(u);
            return u;
        }

        public virtual Task<IUser> GetUserDataForUI(string userId)
        {
            return GetUserData(userId);
        }

        protected virtual Task<IUser> GetUserFromCache(string userId)
        {
            return Cache.Get<IUser>(AddTenantToKey(userId));
        }

        public virtual async Task Save(string userId, IUser user)
        {
            await Cache.Store(AddTenantToKey(userId), user);
        }

        public virtual async Task ClearUserData(string id)
        {
            await Cache.Remove<IUser>(AddTenantToKey(id));
        }


    }
}
