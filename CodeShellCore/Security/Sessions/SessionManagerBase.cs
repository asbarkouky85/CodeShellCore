using CodeShellCore.Data;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Security.Sessions
{
    public abstract class SessionManagerBase
    {
        public SessionManagerBase()
        {
            UsersCache = new Dictionary<object, IUser>();
        }
        protected Dictionary<object, IUser> UsersCache { get; private set; }

        public virtual void ClearUserCache(object id)
        {
            UsersCache.Remove(id);
        }

        public abstract object GetCurrentUserId();

        protected void AppendPermissions(IUser user)
        {

            if (user != null && user is IAuthorizableUser)
            {
                var unit = Shell.ScopedInjector.GetService<ISecurityUnit>();
                if(unit!=null)
                ((IAuthorizableUser)user).Permissions = unit.ResourceRepository.GetUserPermissions(user.UserId);
            }
        }

        protected virtual IUser GetUserFromDataSource(object c)
        {
            var unit = Shell.ScopedInjector.GetService<ISecurityUnit>();
            if (unit != null)
            {
                IUser u = unit.UserRepository.GetByUserId(c);
                AppendPermissions(u);
                if (u == null)
                    return null;
                return u;
            }
            return null;
        }

        public virtual IUser GetUserData()
        {
            object c = GetCurrentUserId();
            if (c == null || c.Equals(0))
                return null;
            IUser u;
            if (!UsersCache.TryGetValue(c, out u))
            {
                u = GetUserFromDataSource(c);
                if (u != null)
                    UsersCache[c] = u;
            }
            return u;
        }
    }
}
