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
        private readonly ISessionManager _manager;

        public AuthorizationService(ISessionManager manager)
        {
            _manager = manager;
        }

        public virtual ISessionManager SessionManager
        {
            get { return _manager; }
        }

        public void AuthorizationRequest(string token = null)
        {
            if (token == null)
                SessionManager.AuthorizationRequest();
            else
                SessionManager.AuthorizationRequest(token);
        }

        public virtual bool IsAuthorized(AuthorizationRequest req)
        {
            return true;
        }

        public virtual void OnUserIsUnauthorized(AuthorizationRequest args)
        {
            
        }
    }
}
