using Asga.Auth;
using Asga.Auth.Data;
using CodeShellCore;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Text;
using CodeShellCore.Web.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Web.Security
{
    public class AsgaAuthenticationService : TokenAuthenticationService
    {
        protected readonly IAuthUnit AuthUnit;


        public AsgaAuthenticationService(IAuthUnit unit, IUserDataService userData) : base(unit, userData)
        {
            AuthUnit = unit;
        }

        public override SubmitResult RequestPasswordReset(ResetPasswordDTO dto)
        {
            if (SecurityUnit.UserRepository.EmailExists(dto.Email))
            {
                User u = AuthUnit.AuthUserRepository.FindSingle(d => d.Email == dto.Email);
                var mails = SecurityUnit.ServiceProvider.GetService<IAuthenticationMailService>();
                
                dto.NewPassword = Utils.RandomAlphaNumeric(8, CharType.Small);
                dto.ServerUrl = Shell.GetConfigAs<string>("ServerUrl", false);
                dto.LogonName = u.LogonName;
                dto.UserFullName = u.Name;

                var res = mails.SendResetEmail(dto);
                if (res.IsSuccess)
                {
                    u.Password = dto.NewPassword.ToMD5();
                    AuthUnit.AuthUserRepository.Update(u);
                    var sRes = AuthUnit.SaveChanges();
                    sRes.Data["EmailRes"] = res;
                    return sRes;
                }
                else
                {
                    var sRes = res.MapToResult<SubmitResult>();
                    sRes.Message = SecurityUnit.TranslateIfMobile("mail_sending_failed");
                    return sRes;
                }

            }
            else
            {
                return new SubmitResult(1, SecurityUnit.TranslateIfMobile("no_such_email"));
            }
        }

        public override SubmitResult ChangePassword(ChangePasswordDTO dto)
        {
            var id = SecurityUnit.UserAccessor.User?.GetUserIdAsLong();
            if (id != null)
            {
                var p = AuthUnit.AuthUserRepository.FindSingle(id);

                if (!string.IsNullOrEmpty(p.Password))
                {
                    if (p.Password.ToLower() != dto.OldPassword.ToMD5())
                    {
                        return new SubmitResult(1, "incorrect_password");
                    }
                }
                p.Password = dto.Password.ToMD5();
                AuthUnit.AuthUserRepository.Update(p);
            }
            return AuthUnit.SaveChanges("password_changed");
        }
    }
}
