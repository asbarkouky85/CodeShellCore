using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Web.Filters;
using CodeShellCore.Web.Security;
using Microsoft.AspNetCore.Mvc;

namespace CodeShellCore.Web.Controllers
{
    [ApiAuthorize(AllowAnonymous = true)]
    public class AccountControllerBase : BaseApiController, IAccountController
    {
        protected IAuthenticationService AuthenticationService => GetService<IAuthenticationService>();
        protected ISessionManager SessionManager => GetService<ISessionManager>();
        protected IUserDataService UserDataService => GetService<IUserDataService>();

        public AccountControllerBase()
        {

        }

        [HttpPost]
        [ApiAuthorize(AllowAnonymous = true)]
        public virtual IActionResult Login([FromBody]LoginModel model)
        {
            return Respond(AuthenticationService.Login(model.UserName, model.Password, model.RememberMe ?? false));
        }

        [ApiAuthorize(AllowAnonymous = true)]
        public virtual IActionResult RefreshToken([FromBody]RefreshTokenDTO refresh)
        {
            var uid = SessionManager.CheckRefreshTokenWEB(refresh.Token);
            LoginResult res = new LoginResult(false, "InvalidToken");
            if (uid != null)
            {
                res = AuthenticationService.LoginById(uid);
            }
            return Respond(res);
        }

        [ApiAuthorize(AllowAll = true, AllowAnonymous = false)]
        public virtual IActionResult GetUserData()
        {
            var res = UserDataService.GetUserDataForUI(SessionManager.GetCurrentUserId());
            return Respond(res);
        }

        [ApiAuthorize(AllowAll = true, AllowAnonymous = false)]
        public virtual IActionResult ChangePassword([FromBody]ChangePasswordDTO dto)
        {
            SubmitResult = AuthenticationService.ChangePassword(dto);
            return Respond();
        }

        [ApiAuthorize(AllowAnonymous = true)]
        public virtual IActionResult SendResetMail(ResetPasswordDTO email)
        {
            SubmitResult = AuthenticationService.RequestPasswordReset(email);
            return Respond();
        }

    }
}
