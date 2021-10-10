using CodeShellCore.Security.Authorization;
using System.Collections.Generic;

namespace CodeShellCore.Security
{
    public interface IAuthorizableUser : IUser
    {
        Dictionary<string, DataAccessPermission> Permissions { get; set; }
        IEnumerable<string> Roles { get; set; }
    }
}
