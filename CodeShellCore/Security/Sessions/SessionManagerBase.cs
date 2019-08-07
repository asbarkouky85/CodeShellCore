using CodeShellCore.Data;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Caching;
using CodeShellCore.Security.Authorization;

namespace CodeShellCore.Security.Sessions
{
    public abstract class SessionManagerBase
    {
        protected readonly IUserDataService UserData;

        public SessionManagerBase(IUserDataService cache)
        {
            UserData = cache;
        }

        public virtual void ClearUserCache(object id)
        {
            UserData.ClearUserData(id);
        }

        public abstract object GetCurrentUserId();
        
        public virtual IUser GetUserData()
        {
            return UserData.GetUserData(GetCurrentUserId());
        }
    }
}
