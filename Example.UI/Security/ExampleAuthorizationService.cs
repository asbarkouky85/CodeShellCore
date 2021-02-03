using CodeShellCore.Security;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Web.Security;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.UI.Security
{
    public class ExampleAuthorizationService : WebAuthorizationService
    {
        public ExampleAuthorizationService(IUserAccessor manager) : base(manager)
        {
        }

        protected override bool IsAuthorized(AuthorizationRequest<AuthorizationFilterContext> req)
        {
            if (User.IsClient)
            {
                if (req.Clients != null && req.Clients.Any(d => d == User.ClientId))
                {
                    return true;
                }
            }
            return User.IsUser;
        }
    }
}
