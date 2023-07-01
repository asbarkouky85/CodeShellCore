using System;

namespace CodeShellCore.Security.Sessions
{
    public interface ISessionManager 
    {
        TimeSpan DefaultSessionTime { get; }
        void StartSession(IUser user, TimeSpan? length = null);
        void EndSession();
        bool IsLoggedIn();
        string GetCurrentUserId();
        string GetConnectionId();
        void AuthorizationRequest();
        void AuthorizationRequest(string token);
        //void ClearUserCache(object id);
        //IUser GetUserData();
    }
}
