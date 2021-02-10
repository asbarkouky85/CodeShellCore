using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;

namespace CodeShellCore.Web.Moldster.Configurator
{
    public class UIAuthorizationService : CodeShellCore.Security.Authorization.AuthorizationService
    {
        public UIAuthorizationService(ISessionManager manager) : base(manager)
        {
        }

        public override bool IsAuthorized(AuthorizationRequest req)
        {
            return true;
        }
    }
}
