﻿using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Security.Sessions;

namespace CodeShellCore.Security.Authorization
{
    public class AuthenticatedOnlyAuthorizationService : AuthorizationService
    {
        public AuthenticatedOnlyAuthorizationService(IUserAccessor manager) : base(manager)
        {
        }

        public override bool IsAuthorized(AuthorizationRequest req)
        {
            return User.IsUser;
        }
    }
}
