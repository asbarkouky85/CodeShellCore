using System;
using System.DirectoryServices.AccountManagement;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Services;

namespace CodeShellCore.Security.Authentication.Internal
{

    public class ActiveDirectoryAuthenticationService : ServiceBase, IAuthenticationService
    {
        //bool _securityUnitChecked = false;
        protected ISecurityUnit _securityUnit;

        public ActiveDirectoryAuthenticationService(ISecurityUnit unit)
        {
            _securityUnit = unit;
        }

        protected virtual void OnLoginAttempt(IUser user)
        {

        }

        public SubmitResult ChangePassword(ChangePasswordDTO dto)
        {
            throw new NotImplementedException();
        }

        public virtual bool Check(string name, string password)
        {
            DomainUser user = new DomainUser(name);
            if (user.Domain == null || user.UserName == null)
                return false;

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, user.Domain))
            {
                return pc.ValidateCredentials(user.UserName, password);

            }
        }

        public virtual LoginResult Login(string name, string password, bool remember = false)
        {
            if (_securityUnit == null)
                throw new Exception("Unit must implement ISecurityUnit to be valid for this function");

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
                return new LoginResult(false, "يرجى التأكد من إدخال إسم المستخدم وكلمة المرور وإعادة المحاولة");

            string[] unameArr = name.Split('\\');

            if (unameArr.Length < 2)
                return new LoginResult(false, "الرجاء كتابه اسم النطاق \\ اسم المستخدم");

            string domain = unameArr[0];
            string userName = unameArr[1];

            IUser CurrentUser = _securityUnit.UserRepository.GetByName(userName);

            if (CurrentUser == null)
                return new LoginResult(false, "المستخدم غير موجود أو غير مفعل");

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain))
            {
                if (pc.ValidateCredentials(userName, password))
                {
                    return new LoginResult(true, "مرحباً بك !", CurrentUser);
                }
                else
                    return new LoginResult(false, "تسجيل الدخول لم ينجح رجاءً تأكد من إسم المُستخدم و كلمة السر");
            }
        }

        public virtual LoginResult LoginById(string id)
        {
            return new LoginResult(false, "Not Supported");
        }

        public virtual SubmitResult RegisterUser(IRegisterModel model)
        {
            return new SubmitResult();
        }

        public SubmitResult RequestPasswordReset(ResetPasswordDTO dto)
        {
            return new SubmitResult();
        }
    }
}
