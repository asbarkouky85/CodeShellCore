using CodeShellCore.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security.Authorization
{
    public interface IUserDataService : IServiceBase
    {
        Dictionary<string, DataAccessPermission> GetRolesPermissions(IEnumerable<object> lst);
        IUser GetUserData(string userId);
        IUser GetUserDataForUI(string userId);
        void ClearUserData(string id);
        void Save(string userId, IUser user);
        void SaveRoleInCache(RoleCacheItem item);
    }
}
