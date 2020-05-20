
using CodeShellCore.Security.Authorization;
using System.Collections.Generic;

namespace CodeShellCore.Security
{
    public interface IUserAccessor
    {
        IUser User { get; }
        object UserId { get; set; }
        T UserAs<T>() where T : class, IUser;
        void Set(IUser user);
    }
}
