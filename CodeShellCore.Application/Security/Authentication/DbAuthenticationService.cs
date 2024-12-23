using CodeShellCore.Data.Helpers;
using CodeShellCore.Services;
using CodeShellCore.Text.Localization;
using CodeShellCore.Types;
using System;
using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;

namespace CodeShellCore.Security.Authentication
{
    public class DbAuthenticationService : ServiceBase, IAuthenticationService
    {
        protected ISecurityUnit SecurityUnit;
        protected InstanceStore Store;
        protected ITokenGenerator TokenGenerator => Store.GetService<ITokenGenerator>();

        public DbAuthenticationService(ISecurityUnit unit)
        {
            SecurityUnit = unit;
            Store = new InstanceStore(() => SecurityUnit.ServiceProvider);
        }

        protected virtual Task OnLoginAttempt(IUser user)
        {
            return Task.CompletedTask;
        }

        public virtual async Task<bool> Check(string name, string password)
        {
            IUser user = await SecurityUnit.UserRepository.GetByCredentials(name, password);
            await OnLoginAttempt(user);
            return user != null;
        }

        public virtual async Task<LoginResult> Login(string name, string password, bool remember = false)
        {
            var checkUsername = name.Contains("\\");

            if (checkUsername)
            {
                return await LoginByActiveDirectory(name, password);
            }

            IUser iuser = await SecurityUnit.UserRepository.GetByCredentials(name, password);
            await OnLoginAttempt(iuser);

            string message = iuser == null ? SecurityUnit.Strings.Message("Invalid_Credentials") : SecurityUnit.Strings.Message("Welcome");
            var result = new LoginResult(iuser != null, message, iuser);
            if (result.IsSuccess)
                TokenGenerator.SetToken(result, SecurityUnit.ClientData?.DeviceId, remember);
            return result;
        }

        private async Task<LoginResult> LoginByActiveDirectory(string name, string password)
        {
            if (SecurityUnit == null)
                throw new Exception("Unit must implement ISecurityUnit to be valid for this function");

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
                return new LoginResult(false, "يرجى التأكد من إدخال إسم المستخدم وكلمة المرور وإعادة المحاولة");

            string[] unameArr = name.Split('\\');

            if (unameArr.Length < 2)
                return new LoginResult(false, "الرجاء كتابه اسم النطاق \\ اسم المستخدم");

            string domain = unameArr[0];
            string userName = unameArr[1];

            IUser CurrentUser = await SecurityUnit.UserRepository.GetByName(userName);

            if (CurrentUser == null)
                return new LoginResult(false, "المستخدم غير موجود أو غير مفعل");

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain))
            {
                if (pc.ValidateCredentials(userName, password))
                {
                    await OnLoginAttempt(CurrentUser);
                    return new LoginResult(true, SecurityUnit.Strings.Message("Welcome"), CurrentUser);
                }
                else
                    return new LoginResult(false, SecurityUnit.Strings.Message("Invalid_Credentials"));
            }
        }

        public virtual async Task<LoginResult> LoginById(string id)
        {
            IUser user = await SecurityUnit.UserRepository.GetByUserId(id);
            await OnLoginAttempt(user);
            string message = user == null ? "اسم المستخدم او كلمه المرور غير صحيحه" : "مرحبا";
            var result = new LoginResult(user != null, message, user);
            if (result.IsSuccess)
                TokenGenerator.SetToken(result, SecurityUnit.ClientData?.DeviceId);
            return result;
        }

        public virtual async Task<SubmitResult> RegisterUser(IRegisterModel model)
        {
            if (await SecurityUnit.UserRepository.NameExists(model.LogonName))
            {
                return new SubmitResult(1, SecurityUnit.Strings.Message(MessageIds.user_name_exists));
            }
            RegisterResult res = await SecurityUnit.UserRepository.AddUser(model);
            if (res.Success)
            {
                var r = await SecurityUnit.SaveChanges();
                r.Data["Model"] = res.Entity;
                return r;
            }
            else
            {
                return new SubmitResult(1, res.Message);
            }

        }

        public virtual Task<SubmitResult> RequestPasswordReset(ResetPasswordDTO dto)
        {
            return Task.Run(() =>
            {
                return new SubmitResult();
            });
        }

        public virtual Task<SubmitResult> ChangePassword(ChangePasswordDTO dto)
        {
            return Task.Run(() =>
            {
                return new SubmitResult();
            });
        }
    }
}
