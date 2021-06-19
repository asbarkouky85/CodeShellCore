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
        protected readonly IUserAccessor User;

        public AuthorizationService(IUserAccessor manager)
        {
            User = manager;
        }

        public virtual bool IsLoggedIn => User.IsUser;

        //public virtual ISessionManager SessionManager
        //{
        //    get { return user; }
        //}

        public virtual bool IsAuthorized(AuthorizationRequest req)
        {
            return true;
        }

        public virtual void OnUserIsUnauthorized(AuthorizationRequest args)
        {
            var ex = new UnauthorizedAccessException();
            ex.Data["Request"] = new
            {
                args.Resource,
                args.Clients,
                args.Action,
                args.Actions
            };
            throw ex;
        }
    }
}
