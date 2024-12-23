using CodeShellCore.Data.Helpers;
using CodeShellCore.Services;
using System.Threading.Tasks;

namespace CodeShellCore.Security.Authentication
{
    public interface IAuthenticationService : IServiceBase
    {
        Task<LoginResult> Login(string name, string password, bool remember = false);
        Task<bool> Check(string name, string password);
        Task<LoginResult> LoginById(string id);
        Task<SubmitResult> RegisterUser(IRegisterModel model);
        Task<SubmitResult> RequestPasswordReset(ResetPasswordDTO dto);
        Task<SubmitResult> ChangePassword(ChangePasswordDTO dto);

    }
}
