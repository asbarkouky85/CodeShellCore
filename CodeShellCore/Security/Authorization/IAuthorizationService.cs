using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Security.Authorization
{
    public interface IAuthorizationService
    {
        IAuthenticationService AuthenticationService { get; }
        
        ISessionManager SessionManager { get; }
        LoginResult Login(LoginModel mod);
        void AuthorizationRequest(string token = null);
        void LogOut();
    }
}
