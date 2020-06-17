using CodeShellCore.Data;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Caching;
using CodeShellCore.Security.Authorization;

namespace CodeShellCore.Security.Sessions
{
    public abstract class SessionManagerBase
    {

        public SessionManagerBase()
        {
        }

        public abstract string GetCurrentUserId();

        public virtual string GetConnectionId()
        {
            return null;
        }

        public virtual void EndSession()
        {
            
        }
    }
}
