﻿using Asga.Auth;
using Asga.Security;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Web.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Asga.Web.Security
{
    public class AsgaAuthorizationService : AccessControlAuthorizationService, IAccessControlAuthorizationService
    {
        private readonly ISessionManager _manager;
        private UserDTO User;

        public AsgaAuthorizationService(IUserAccessor acc, ISessionManager manager) : base(manager)
        {
            _manager = manager;
            User = acc.User as UserDTO;
        }

        protected override bool IsAuthorized(AuthorizationRequest<AuthorizationFilterContext> req)
        {
            if (User == null)
                return false;
            if (User.Permissions.TryGetValue(req.Resource, out DataAccessPermission perm))
            {
                var permissions = perm;
                switch (req.Action)
                {
                    case "CanViewDetails":
                        return permissions.Details;
                    case "CanInsert":
                        return permissions.Insert;
                    case "CanUpdate":
                        return permissions.Update;
                    case "CanViewDelete":
                        return permissions.Delete;
                    case "Get":
                        return permissions != null;
                }
            }

            return false;
            //return SessionManager.IsLoggedIn();
        }



    }
}
