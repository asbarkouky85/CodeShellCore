using CodeShellCore.Data;
using CodeShellCore.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Security
{
    public interface IAuthorizableUser : IUser
    {
        Dictionary<string, Permission> Permissions { get; set; }
    }
}
