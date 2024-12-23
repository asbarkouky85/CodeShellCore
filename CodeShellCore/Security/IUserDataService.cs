using CodeShellCore.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Security.Authorization
{
    public interface IUserDataService : IServiceBase
    {
        Task<Dictionary<string, DataAccessPermission>> GetRolesPermissions(IEnumerable<string> lst);
        Task<IUser> GetUserData(string userId);
        Task<IUser> GetUserDataForUI(string userId);
        Task ClearUserData(string id);
        Task Save(string userId, IUser user);
        Task SaveRoleInCache(RoleCacheItem item);
    }
}
