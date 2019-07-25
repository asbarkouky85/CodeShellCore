using CodeShellCore;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Web.Security;
using CodeShellCore.Text;
using Asga.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Security;
using CodeShellCore.Text.Localization;
using CodeShellCore.Data;
using System.Linq;
using CodeShellCore.Data.EntityFramework;
using CodeShellCore.MQ;
using Asga.Auth.Dto;
using CodeShellCore.Security.Sessions;
using Asga.Auth.Services;

namespace Asga.Auth.Security
{
    public class AuthServerAuthenticationService : RoleBasedTokenAuthenticationService
    {
        private readonly IRoleBasedSecurityUnit _unit;

        public AuthServerAuthenticationService(IRoleBasedSecurityUnit unit,ISessionManager man) : base(unit,man)
        {
            _unit = unit;
        }

        protected override string TokenProvider
        {
            get
            {
                return "";// AuthServerHttpService.ServiceProvider;
            }
        }

        public override LoginResult Login(string name, string password)
        {

            var res = base.Login(name, password);

            if (res.Success)
            {
                var u = res.UserData as UserDTO;

                if (u.PartyId != null && !u.Customers.Any())
                {
                    Shell.ScopedInjector.GetRequiredService<UsersService>().UpdateUserParties(u.Id, u.PartyId.Value);
                }
                var conf = Shell.GetConfigAs<TestingDB>("TestingDB", false);
                if (conf != null)
                {
                    u.TenantCode = conf.FromTenantCode;
                    u.TenantId = conf.FromTenantId;
                }

                if (_unit is AuthUnit)
                {
                    var userCacheDto = ((AuthUnit)_unit).AuthUserRepository.FindSingleAs(UserCacheDTO.Expression, res.UserData.UserId);
                    Publish(userCacheDto);
                }


            }
            return res;
        }

        protected override JWTData MakeJWT(LoginResult res)
        {
            var s = base.MakeJWT(res);

            var conf = Shell.GetConfigAs<TestingDB>("TestingDB", false);


            return new AsgaJWTData
            {
                ExpireTime = s.ExpireTime,
                StartTime = s.StartTime,
                Provider = s.Provider,
                TenantCode = conf != null ? conf.FromTenantCode : ((UserDTO)res.UserData).TenantCode,
                TenantId = conf != null ? conf.FromTenantId : ((UserDTO)res.UserData).TenantId,
                UserId = s.UserId,
                Roles = s.Roles

            };
        }

        public void Logout(long userId)
        {

        }

        private void Publish(UserCacheDTO obj)
        {
            Transporter.Publish(obj);
        }
    }
}
