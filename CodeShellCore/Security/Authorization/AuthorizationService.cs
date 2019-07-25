using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Data;
using CodeShellCore.Services;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Data.Helpers;
using System;

namespace CodeShellCore.Security.Authorization
{
    public class AuthorizationService : ServiceBase, IAuthorizationService
    {
        private readonly IAuthenticationService _auth;
        private readonly ISessionManager _manager;

        public AuthorizationService(IAuthenticationService auth, ISessionManager manager) {
            _auth = auth;
            _manager = manager;
        }
        public object GetCurrentUserId()
        {
            return SessionManager?.GetCurrentUserId();
        }

        public virtual ISessionManager SessionManager
        {
            get { return _manager; }
        }

        public virtual IAuthenticationService AuthenticationService
        {
            get { return _auth; }
        }

        public virtual LoginResult Login(LoginModel mod)
        {
            LoginResult res = AuthenticationService.Login(mod.UserName, mod.Password);
            if (res.Success)
                SessionManager.StartSession(res.UserData);
            return res;
        }

        public virtual SubmitResult Register(IRegisterModel model)
        {
            return AuthenticationService?.RegisterUser(model);
        }

        public virtual void OnUserLogin(IUser user) { }

        public virtual void LogOut()
        {
            SessionManager.EndSession();
        }

        public void AuthorizationRequest(string token = null)
        {
            if (token == null)
                SessionManager.AuthorizationRequest();
            else
                SessionManager.AuthorizationRequest(token);
        }
    }
}
