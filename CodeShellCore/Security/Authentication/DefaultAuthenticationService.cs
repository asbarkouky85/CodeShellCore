using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeShellCore.Data;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Text.Localization;
using CodeShellCore.Services;

namespace CodeShellCore.Security.Authentication
{
    public class DefaultAuthenticationService : ServiceBase, IAuthenticationService
    {


        ISecurityUnit SecurityUnit;

        public DefaultAuthenticationService(ISecurityUnit unit)
        {
            SecurityUnit = unit;
        }

        protected void AppendPermissions(IUser user)
        {
            if (user != null)
            {
                if (user is IAuthorizableUser)
                    ((IAuthorizableUser)user).Permissions = SecurityUnit.ResourceRepository.GetUserPermissions(user.UserId);
                if (user is IEntityLinkedUser)
                    ((IEntityLinkedUser)user).EntityLinks = SecurityUnit.UsersEntityLinkRepository.GetUserLinks(user.UserId);
            }
        }

        public virtual bool Check(string name, string password)
        {
            if (SecurityUnit == null)
                throw new Exception("Unit must implement ISecurityUnit to be valid for this function");

            IUser user = SecurityUnit.UserRepository.GetByCredentials(name, password);
            AppendPermissions(user);
            return user != null;
        }

        public virtual LoginResult Login(string name, string password)
        {
            if (SecurityUnit == null)
                throw new Exception("Unit must implement ISecurityUnit to be valid for this function");

            IUser user = SecurityUnit.UserRepository.GetByCredentials(name, password);
            AppendPermissions(user);
            string message = user == null ? Strings.Message("Invalid_Credentials") : Strings.Message("Welcome");
            return new LoginResult(user != null, message, user);
        }

        public virtual LoginResult LoginById(object id)
        {
            if (SecurityUnit == null)
                throw new Exception("Unit must implement ISecurityUnit to be valid for this function");

            IUser user = SecurityUnit.UserRepository.GetByUserId(id);
            AppendPermissions(user);
            string message = user == null ? "اسم المستخدم او كلمه المرور غير صحيحه" : "مرحبا";
            return new LoginResult(user != null, message, user);
        }

        public virtual SubmitResult RegisterUser(IRegisterModel model)
        {
            if (SecurityUnit.UserRepository.NameExists(model.LogonName))
            {
                return new SubmitResult(1, Strings.Message(MessageIds.user_name_exists));
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
