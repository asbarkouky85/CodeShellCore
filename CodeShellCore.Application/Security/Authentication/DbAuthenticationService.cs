using CodeShellCore.Data.Helpers;
using CodeShellCore.Services;
using CodeShellCore.Text.Localization;
using CodeShellCore.Types;
using System;
using System.DirectoryServices.AccountManagement;

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
            var checkUsername = name.Contains("\\");

            if (checkUsername)
            {
                return LoginByActiveDirectory(name, password);
            }

            IUser iuser = SecurityUnit.UserRepository.GetByCredentials(name, password);
            OnLoginAttempt(iuser);

            string message = iuser == null ? SecurityUnit.Strings.Message("Invalid_Credentials") : SecurityUnit.Strings.Message("Welcome");
            var result = new LoginResult(iuser != null, message, iuser);
            if (result.IsSuccess)
                TokenGenerator.SetToken(result, SecurityUnit.ClientData?.DeviceId, remember);
            return result;
        }

        private LoginResult LoginByActiveDirectory(string name, string password)
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

            IUser CurrentUser = SecurityUnit.UserRepository.GetByName(userName);

            if (CurrentUser == null)
                return new LoginResult(false, "المستخدم غير موجود أو غير مفعل");

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain))
            {
                if (pc.ValidateCredentials(userName, password))
                {
                    OnLoginAttempt(CurrentUser);
                    return new LoginResult(true, SecurityUnit.Strings.Message("Welcome"), CurrentUser);
                }
                else
                    return new LoginResult(false, SecurityUnit.Strings.Message("Invalid_Credentials"));
            }
        }

        public virtual LoginResult LoginById(string id)
        {
            IUser user = SecurityUnit.UserRepository.GetByUserId(id);
            OnLoginAttempt(user);
            string message = user == null ? "اسم المستخدم او كلمه المرور غير صحيحه" : "مرحبا";
            var result = new LoginResult(user != null, message, user);
            if (result.IsSuccess)
                TokenGenerator.SetToken(result, SecurityUnit.ClientData?.DeviceId);
            return result;
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
