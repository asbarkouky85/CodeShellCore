using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Security
{
    public interface IUsersEntityLinkRepository : IRepository
    {
        Task<Dictionary<string,IEnumerable<long>>> GetUserLinks(object userId);
    }
}
