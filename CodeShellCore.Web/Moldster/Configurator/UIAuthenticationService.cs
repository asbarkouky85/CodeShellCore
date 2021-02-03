using CodeShellCore.Helpers;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Web.Security;

namespace CodeShellCore.Web.Moldster.Configurator
{
    public class UIAuthenticationService : TokenAuthenticationService
    {
        public UIAuthenticationService(IUserDataService userData) : base(null, userData)
        {
        }

        public override LoginResult LoginById(string id)
        {
            var dto = new ConfiguratorUserDTO();
            var res = new LoginResult(true, "Welcome", dto);
            SetToken(res);
            return res;
        }
        public override bool Check(string name, string password)
        {
            return name == "admin" && password == "963258741";
        }

        public override LoginResult Login(string name, string password, bool remember = false)
        {
            if (Check(name, password))
            {
                var dto = new ConfiguratorUserDTO();
                var res = new LoginResult(true, "Welcome", dto);
                SetToken(res, remember);
                return res;
            }
            return new LoginResult(false, "Incorrect user name or password");
        }

    }
}
