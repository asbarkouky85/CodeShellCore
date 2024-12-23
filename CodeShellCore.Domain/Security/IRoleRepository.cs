using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CodeShellCore.Data;

namespace CodeShellCore.Security
{
    public interface IRoleRepository : IRepository
    {
        Task<IEnumerable<string>> GetUserRoles(object userId);

   }
}
