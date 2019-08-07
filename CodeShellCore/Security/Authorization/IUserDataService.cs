using CodeShellCore.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security.Authorization
{
    public interface IUserDataService : IServiceBase
    {
        Dictionary<string, Permission> GetRolesPermissions(IEnumerable lst);
        IUser GetUserData(object userId);
        void ClearUserData(object id);
        void Save(object userId, IUser user);
    }
}
