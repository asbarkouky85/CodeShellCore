using CodeShellCore.Data.Helpers;
using CodeShellCore.Services;
using CodeShellCore.Text.Localization;

namespace CodeShellCore.Security.Authentication.Internal
{
    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        protected ISecurityUnit SecurityUnit;

        public AuthenticationService(ISecurityUnit unit)
        {
            SecurityUnit = unit;
        }

        protected virtual void OnLoginAttempt(IUser user)
        {

        }

        public virtual bool Check(string name, string password)
        {
            IUser user = SecurityUnit.UserRepository.GetByCredentials(name, password);
            OnLoginAttempt(user);
            return user != null;
        }

        public virtual LoginResult Login(string name, string password, bool remember = false)
        {
            IUser user = SecurityUnit.UserRepository.GetByCredentials(name, password);
            OnLoginAttempt(user);
            string message = user == null ? SecurityUnit.Strings.Message("Invalid_Credentials") : SecurityUnit.Strings.Message("Welcome");
            return new LoginResult(user != null, message, user);
        }

        public virtual LoginResult LoginById(string id)
        {
            IUser user = SecurityUnit.UserRepository.GetByUserId(id);
            OnLoginAttempt(user);
            string message = user == null ? "اسم المستخدم او كلمه المرور غير صحيحه" : "مرحبا";
            return new LoginResult(user != null, message, user);
        }

        public virtual SubmitResult RegisterUser(IRegisterModel model)
        {
            if (SecurityUnit.UserRepository.NameExists(model.LogonName))
            {
                return new SubmitResult(1, SecurityUnit.Strings.Message(MessageIds.user_name_exists));
            }
            RegisterResult res = SecurityUnit.UserRepository.AddUser(model);
            if (res.Success)
            {
                var r = SecurityUnit.SaveChanges();
                r.Data["Model"] = res.Entity;
                return r;
            }
            else
            {
                return new SubmitResult(1, res.Message);
            }

        }

        public virtual SubmitResult RequestPasswordReset(ResetPasswordDTO dto)
        {

            return new SubmitResult();
        }

        public virtual SubmitResult ChangePassword(ChangePasswordDTO dto)
        {
            return new SubmitResult();
        }
    }
}
