using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;

namespace CodeShellCore.Moldster.Security
{
    public class UIAuthenticationService : DbAuthenticationService
    {
        public UIAuthenticationService(ISecurityUnit unit) : base(unit)
        {
        }

        public override LoginResult LoginById(string id)
        {
            var dto = new ConfiguratorUserDTO();
            var res = new LoginResult(true, "Welcome", dto);
            TokenGenerator.SetToken(res);
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
                TokenGenerator.SetToken(res, null, remember);
                return res;
            }
            return new LoginResult(false, "Incorrect user name or password");
        }

    }
}
