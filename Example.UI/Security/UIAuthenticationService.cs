using CodeShellCore.Data.Helpers;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Web.Moldster.Configurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.UI.Security
{
    public class UIAuthenticationService : IAuthenticationService
    {
        public SubmitResult ChangePassword(ChangePasswordDTO dto)
        {
            return new SubmitResult();
        }

        public bool Check(string name, string password)
        {
            return true;
        }

        public void Dispose()
        {

        }

        public LoginResult Login(string name, string password, bool remember = false)
        {
            if (name == "admin" && password == "12345")
                return new LoginResult(true, "Welcome", new ConfiguratorUserDTO());
            return new LoginResult(false, "Failed");
        }

        public LoginResult LoginById(string id)
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
