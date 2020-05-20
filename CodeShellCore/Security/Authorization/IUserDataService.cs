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
        IUser GetUserData(object userId);
        IUser GetUserDataForUI(object userId);
        void ClearUserData(object id);
        void Save(object userId, IUser user);
        void SaveRoleInCache(RoleCacheItem item);
    }
}
