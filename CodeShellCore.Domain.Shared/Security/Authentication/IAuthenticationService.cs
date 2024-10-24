using CodeShellCore.Data.Helpers;
using CodeShellCore.Services;

namespace CodeShellCore.Security.Authentication
{
    public interface IAuthenticationService : IServiceBase
    {
        LoginResult Login(string name, string password, bool remember = false);
        bool Check(string name, string password);
        LoginResult LoginById(string id);
        SubmitResult RegisterUser(IRegisterModel model);
        SubmitResult RequestPasswordReset(ResetPasswordDTO dto);
        SubmitResult ChangePassword(ChangePasswordDTO dto);

    }
}
