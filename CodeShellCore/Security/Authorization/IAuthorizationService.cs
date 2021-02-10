using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Security.Authorization
{
    public interface IAuthorizationService : IServiceBase
    {
        ISessionManager SessionManager { get; }
        bool IsAuthorized(AuthorizationRequest req);
        void OnUserIsUnauthorized(AuthorizationRequest args);
    }
}
