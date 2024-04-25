using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security
{
    public interface IUsersEntityLinkRepository : IRepository
    {
        Dictionary<string, IEnumerable<long>> GetUserLinks(object userId);
    }
}
