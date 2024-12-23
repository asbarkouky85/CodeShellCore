using System;
using System.Threading.Tasks;

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
        Task AuthorizationRequest();
        void UseToken(string token);
        //void ClearUserCache(object id);
        //IUser GetUserData();
    }
}
