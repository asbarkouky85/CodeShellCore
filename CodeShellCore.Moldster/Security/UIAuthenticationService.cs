using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using System.Threading.Tasks;

namespace CodeShellCore.Moldster.Security
{
    public class UIAuthenticationService : DbAuthenticationService
    {
        public UIAuthenticationService(ISecurityUnit unit) : base(unit)
        {
        }

        public override Task<LoginResult> LoginById(string id)
        {
            return Task.Run(() =>
            {

                var dto = new ConfiguratorUserDTO();
                var res = new LoginResult(true, "Welcome", dto);
                TokenGenerator.SetToken(res);
                return res;
            });
        }
        public override Task<bool> Check(string name, string password)
        {
            return Task.Run(() =>
            {
                return name == "admin" && password == "963258741";
            });
        }

        public override async Task<LoginResult> Login(string name, string password, bool remember = false)
        {
            if (await Check(name, password))
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
