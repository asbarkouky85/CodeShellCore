using System;

namespace CodeShellCore.Security.Sessions
{
    public abstract class SessionManagerBase
    {

        public SessionManagerBase(IServiceProvider provider)
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
