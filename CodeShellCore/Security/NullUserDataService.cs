using CodeShellCore.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Security
{
    public class NullUserDataService : IUserDataService
    {
        public Task ClearUserData(string id)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }

        public Task<Dictionary<string, DataAccessPermission>> GetRolesPermissions(IEnumerable<string> lst)
        {
            return Task.Run(() =>
            {
                return new Dictionary<string, DataAccessPermission>();
            });
        }

        public Task<IUser> GetUserData(string userId)
        {
            return Task.Run(() => { return (IUser)null; });
        }

        public Task<IUser> GetUserDataForUI(string userId)
        {
            return Task.Run(() => { return (IUser)null; });
        }

        public Task Save(string userId, IUser user)
        {
            return Task.CompletedTask;
        }

        public Task SaveRoleInCache(RoleCacheItem item)
        {
            return Task.CompletedTask;
        }
    }
}
