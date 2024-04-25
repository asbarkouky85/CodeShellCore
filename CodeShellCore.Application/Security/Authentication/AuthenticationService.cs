using CodeShellCore.Data.Helpers;
using CodeShellCore.Services;
using CodeShellCore.Text.Localization;
using System.DirectoryServices.AccountManagement;
using System;
using System.Collections.Generic;
using CodeShellCore.Text;

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
            var checkUsername = name.Contains("\\");
            if (checkUsername)
            {
                return LoginByActiveDirectory(name, password);
            }

            IUser iuser = SecurityUnit.UserRepository.GetByCredentials(name, password);
            OnLoginAttempt(iuser);
            string message = iuser == null ? SecurityUnit.Strings.Message("Invalid_Credentials") : SecurityUnit.Strings.Message("Welcome");
            return new LoginResult(iuser != null, message, iuser);
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
