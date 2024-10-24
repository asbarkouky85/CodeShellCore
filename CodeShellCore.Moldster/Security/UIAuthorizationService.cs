using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeShellCore.Security;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;

namespace CodeShellCore.Web.Moldster.Configurator
{
    public class UIAuthorizationService : AuthorizationService
    {
        public UIAuthorizationService(IUserAccessor manager) : base(manager)
        {
        }

        public override bool IsAuthorized(AuthorizationRequest req)
        {
            return true;
        }
    }
}
