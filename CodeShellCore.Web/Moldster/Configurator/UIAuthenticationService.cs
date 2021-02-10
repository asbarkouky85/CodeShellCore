using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeShellCore;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Text;
using CodeShellCore.Web.Security;

namespace CodeShellCore.Web.Moldster.Configurator
{
    public class UIAuthenticationService : CodeShellCore.Security.Authentication.IAuthenticationService
    {
        DefaultAuthenticationService def;
        public UIAuthenticationService(ISessionManager manager)
        {
            def = new TokenAuthenticationService(null, manager);

        }

        public SubmitResult ChangePassword(ChangePasswordDTO dto)
        {
            return new SubmitResult();
        }

        public bool Check(string name, string password)
        {
            return name == "admin" && password == "963258741";
        }

        public void Dispose()
        {

        }

        public LoginResult Login(string name, string password)
        {
            if (Check(name, password))
            {
                var dto = new ConfiguratorUserDTO();
                var res = new LoginResult(true, "Welcome", dto);
                var jwt = new JWTData
                {
                    UserId = dto.UserId,
                    ExpireTime = DateTime.Now.AddDays(1),
                    StartTime = DateTime.Now,
                    Name = dto.Name
                };
                res.Token = Shell.Encryptor.Encrypt(jwt.ToJson());
                res.TokenExpiry = jwt.ExpireTime;
                return res;
            }
            return new LoginResult(false, "Incorrect user name or password");
        }

        public LoginResult LoginById(object id)
        {
            return new LoginResult(true, "Welcome", new ConfiguratorUserDTO());
        }

        public SubmitResult RegisterUser(IRegisterModel model)
        {
            return new SubmitResult();
        }

        public SubmitResult RequestPasswordReset(ResetPasswordDTO dto)
        {
            return new SubmitResult();
        }
    }
}
