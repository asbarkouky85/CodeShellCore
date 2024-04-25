using CodeShellCore.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security
{
    public class NullUserDataService : IUserDataService
    {
        public void ClearUserData(string id)
        {
            
        }

        public void Dispose()
        {
            
        }

        public Dictionary<string, DataAccessPermission> GetRolesPermissions(IEnumerable<string> lst)
        {
            return new Dictionary<string, DataAccessPermission>();
        }

        public IUser GetUserData(string userId)
        {
            return null;
        }

        public IUser GetUserDataForUI(string userId)
        {
            return null;
        }

        public void Save(string userId, IUser user)
        {
            
        }

        public void SaveRoleInCache(RoleCacheItem item)
        {
            
        }
    }
}
