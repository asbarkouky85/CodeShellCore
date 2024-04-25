using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Security
{
    public interface IUser
    {
        string UserId { get; }
        string Name { get; }
        string LogonName { get; }
        string App { get; }
        bool? IsActiveDirectory { get; }

        long? GetUserIdAsLong();
        string GetPersonId();
    }
}
