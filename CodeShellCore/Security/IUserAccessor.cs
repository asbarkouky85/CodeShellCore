
using CodeShellCore.Security.Authorization;
using System.Collections.Generic;

namespace CodeShellCore.Security
{
    public interface IUserAccessor
    {
        IUser User { get; }
        string UserId { get; set; }
        string ClientId { get; set; }
        T UserAs<T>() where T : class, IUser;
        bool IsUser { get; }
        bool IsClient { get; }
        void Set(IUser user);
    }
}
