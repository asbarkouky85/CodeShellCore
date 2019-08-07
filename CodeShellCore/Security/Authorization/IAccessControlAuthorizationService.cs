using CodeShellCore.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Security.Authorization
{
    public interface IAccessControlAuthorizationService : IAuthorizationService
    {
        bool IsAuthorized(AuthorizationRequest req);
        void OnUserIsUnauthorized(AuthorizationRequest args);
    }
}
