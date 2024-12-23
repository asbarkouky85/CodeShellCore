using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Web.Filters;
using CodeShellCore.Web.Security;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Controllers
{
    [ApiAuthorize(AllowAnonymous = true)]
    public abstract class AccountControllerBase : BaseApiController, IAccountController

    {
        protected IAuthenticationService AuthenticationService => GetService<IAuthenticationService>();
        protected ISessionManager SessionManager => GetService<ISessionManager>();
        protected IUserDataService UserDataService => GetService<IUserDataService>();

        public AccountControllerBase()
        {

        }

        [HttpPost]
        [ApiAuthorize(AllowAnonymous = true)]
        public virtual Task<LoginResult> Login([FromBody] LoginModel model)
        {
            return AuthenticationService.Login(model.UserName, model.Password, model.RememberMe ?? false);
        }

        [ApiAuthorize(AllowAnonymous = true)]
        public virtual async Task<LoginResult> RefreshToken([FromBody] RefreshTokenDTO refresh)
        {
            var uid = SessionManager.CheckRefreshTokenWEB(refresh.Token);
            LoginResult res = new LoginResult(false, "InvalidToken");
            if (uid != null)
            {
                res = await AuthenticationService.LoginById(uid);
            }
            return res;
        }

        [ApiAuthorize(AllowAll = true, AllowAnonymous = false)]
        public virtual async Task<object> GetUserData()
        {
            var res = await UserDataService.GetUserDataForUI(SessionManager.GetCurrentUserId());
            return res;
        }

        [ApiAuthorize(AllowAll = true, AllowAnonymous = false)]
        public virtual async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            SubmitResult = await AuthenticationService.ChangePassword(dto);
            return Respond();
        }

        [ApiAuthorize(AllowAnonymous = true)]
        public virtual async Task<IActionResult> SendResetMail(ResetPasswordDTO email)
        {
            SubmitResult = await AuthenticationService.RequestPasswordReset(email);
            return Respond();
        }

    }
}
