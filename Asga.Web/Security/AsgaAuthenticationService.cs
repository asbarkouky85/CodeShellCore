using Asga.Auth.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Text;
using CodeShellCore.Web.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Web.Security
{
    public class AsgaAuthenticationService : TokenAuthenticationService
    {
        private readonly AuthUnit unit;
        private readonly ISessionManager sessionManager;

        public AsgaAuthenticationService(AuthUnit unit, ISessionManager sessionManager, IUserDataService userData) : base(unit, sessionManager, userData)
        {
            this.unit = unit;
            this.sessionManager = sessionManager;
        }

        
        public override SubmitResult ChangePassword(ChangePasswordDTO dto)
        {
            if (long.TryParse(sessionManager.GetCurrentUserId(), out long id))
            {
                var p = unit.AuthUserRepository.FindSingle(id);

                if (!string.IsNullOrEmpty(p.Password))
                {
                    if (p.Password.ToLower() != dto.OldPassword.ToMD5())
                    {
                        return new SubmitResult(1, "incorrect_password");
                    }
                }
                p.Password = dto.Password.ToMD5();
                unit.AuthUserRepository.Update(p);
            }
            return unit.SaveChanges("password_changed");
        }
    }
}
