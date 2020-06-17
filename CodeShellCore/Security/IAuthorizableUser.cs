using CodeShellCore.Data;
using CodeShellCore.Security.Authorization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Security
{
    public interface IAuthorizableUser : IUser
    {
        Dictionary<string, DataAccessPermission> Permissions { get; set; }
        IEnumerable<object> Roles { get; set; }
    }
}
