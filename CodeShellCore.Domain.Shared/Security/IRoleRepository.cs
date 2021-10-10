using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Data;

namespace CodeShellCore.Security
{
    public interface IRoleRepository : IRepository
    {
        IEnumerable<string> GetUserRoles(object userId);

   }
}
