using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Data;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Sessions;

namespace CodeShellCore.Web.Security
{
    public class RoleBasedTokenAuthenticationService : TokenAuthenticationService
    {
        private readonly IRoleBasedSecurityUnit _unit;
        public RoleBasedTokenAuthenticationService(IRoleBasedSecurityUnit unit, ISessionManager man) : base(unit, man)
        {
            _unit = unit;
        }

        protected override JWTData MakeJWT(LoginResult res)
        {
            var jwt = base.MakeJWT(res);
            jwt.Roles = _unit.SecurityRoleRepository.GetUserRoles(res.UserData.UserId);
            return jwt;
        }
    }
}
