using CodeShellCore.Data;
using CodeShellCore.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security
{
    public interface IResourceRepository : IRepository
    {
        Dictionary<string, Permission> GetUserPermissions(object userId);
    }
}
