using CodeShellCore.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Security.Sessions
{
    public class TestSessionManager : SessionManagerBase, ISessionManager
    {
        private object currentUserId;

        //public TimeSpan DefaultSessionTime => throw new NotImplementedException();
        public TimeSpan DefaultSessionTime { get { return new TimeSpan(1, 0, 0); } set { } }//DefaultSessionTime= value;

        public override object GetCurrentUserId()
        {
            return currentUserId;
        }

        public void StartSession(IUser user, TimeSpan? length = null)
        {
            currentUserId = user.UserId;
        }

        public bool IsLoggedIn()
        {
            return true;
        }

        public void AuthorizationRequest()
        {
            throw new NotImplementedException();
        }

        public void AuthorizationRequest(string token)
        {
            throw new NotImplementedException();
        }

        public TestSessionManager(object userId)
        {
            currentUserId = userId;
        }
    }
}
