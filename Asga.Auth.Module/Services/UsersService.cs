using Asga.Auth.Data;
using Asga.Security;
using CodeShellCore;
using CodeShellCore.Caching;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Security;
using CodeShellCore.Http;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Asga.Auth.Services
{
    public class UsersService : AsgaEntityService<User>, IUsersService
    {
        private AuthUnit Unit;

        private readonly IUserAccessor _user;
        private readonly ICacheProvider cache;
        private static Dictionary<long, string> ResetUserTokens = new Dictionary<long, string>();
        public UsersService(
            AuthUnit unit,
            IUserAccessor user,
            ICacheProvider cache
            ) : base(unit)
        {
            Unit = unit;
            _user = user;
            this.cache = cache;
        }

        #region Overrides

        public override User GetSingle(object id)
        {
            var u = base.GetSingle(id);


            if (u != null)
            {
                u.Role = Unit.RoleRepository.GetUserSpecializedRole(u.Id);
                u.UserRoles = Unit.UserRoleRepository.Find(d => d.UserId == u.Id);
                u.Password = null;
                if (u.Role != null)
                {
                    u.Role.RoleResourceActions = Unit.RoleResourceActionRepository.Find(d => d.RoleId == u.Role.Id);
                    u.Role.RoleResources = Unit.RoleResourceRepository.Find(d => d.RoleId == u.Role.Id);
                }
            }

            return u;
        }


        public override SubmitResult Create(User obj)
        {
            obj.Password = obj.Password?.ToMD5();

            if (obj.Role != null)
            {
                var rl = obj.Role;
                rl.AddUser(obj);
                foreach (var x in obj.Role.RoleResources)
                    x.Id = Utils.GenerateID();
                foreach (var x in obj.Role.RoleResourceActions)
                    x.Id = Utils.GenerateID();
                Unit.RoleRepository.Add(rl);
            }

            if (obj.AppId != null)
            {
                var appRole = Unit.RoleRepository.FindSingle(d => d.IsDefaultAppRole == true && d.TenantAppId == obj.AppId);
                if (appRole != null)
                    appRole.AddUser(obj);
            }

            if (obj.RoleId != null)
            {
                obj.UserRoles.Add(new UserRole { RoleId = obj.RoleId.Value });
            }

            var r = base.Create(obj);
            if (r.IsSuccess)
                obj.PhotoFile?.DeleteTmp();
            return r;
        }

        public override SubmitResult Update(User obj)
        {
            User entity = Repository.FindSingle(obj.Id);
            if (!string.IsNullOrEmpty(obj.Password))
            {
                entity.Password = obj.Password.ToMD5();
                Repository.Update(obj);
            }
            else
            {
                entity = Repository.FindSingle(obj.Id);
                entity.AppendProperties(obj, true, new[] { "Password" });
            }

            Unit.UserRoleRepository.ApplyChanges(obj.Roles);

            if (obj.Role != null)
            {
                Unit.RoleResourceRepository.ApplyChanges(obj.Role.RoleResources);
                Unit.RoleResourceActionRepository.ApplyChanges(obj.Role.RoleResourceActions);
            }

            if (obj.UserRoles != null)
                Unit.UserRoleRepository.ApplyChanges(obj.UserRoles);

            if (obj.RoleId != null)
            {
                Unit.UserRoleRepository.Delete(d => d.UserId == obj.Id);
                Unit.UserRoleRepository.Add(new UserRole
                {
                    RoleId = obj.RoleId.Value,
                    UserId = obj.Id
                });
            }

            var ch = base.Update(entity);
            if (ch.Code == 0)
            {
                cache.Remove<IUser>(obj.Id.ToString());
                obj.PhotoFile?.DeleteTmp();
            }
            return ch;
        }

        #endregion
        public virtual User GetUserByUserName(string userName)
        {
            try
            {
                var user = Repository.FindSingle(x => x.LogonName == userName);

                return user;

            }
            catch (Exception)
            {
                return null;
            }


        }


        public long SoftDelete(long id, bool isDeleted)
        {
            try
            {
                var user = Repository.FindSingle(x => x.Id == id);
                if (user != null)
                {
                    user.IsDeleted = isDeleted;
                    Repository.Update(user);
                    var result = Unit.SaveChanges();
                    if (result.AffectedRows > 0)
                    {
                        return result.AffectedRows;
                    }

                }
                return 0;

            }
            catch (Exception)
            {
                return 0;
            }


        }

        public virtual Role GetUserRole(long id)
        {

            var role = Unit.RoleRepository.FindSingle(r => r.UserRoles.Select(d => d.UserId).Contains(id) && r.IsUserRole);
            if (role == null)
            {
                role = new Role
                {
                    Name = "RoleForUser__" + id,
                    IsUserRole = true
                };
                var uRole = new UserRole
                {
                    Id = Utils.GenerateID(),
                    UserId = id
                };
                if (id != 0)
                {
                    role.UserRoles.Add(uRole);
                    Unit.RoleRepository.Add(role);
                    uRole.Role = null;
                    var res = Unit.SaveChanges();
                }
            }
            else
            {
                role.RoleResources = Unit.RoleResourceRepository.Find(d => d.RoleId == role.Id);
                role.RoleResourceActions = Unit.RoleResourceActionRepository.Find(d => d.RoleId == role.Id);
                foreach (var r in role.RoleResources)
                {
                    r.Role = null;
                }

                foreach (var r in role.RoleResourceActions)
                {
                    r.Role = null;
                }
            }
            return role;
        }

        public virtual SubmitResult ChangePassword(CodeShellCore.Security.Authentication.ChangePasswordDTO dto)
        {
            var id = _user.User.GetUserIdAsLong();
            var u = Unit.AuthUserRepository.FindSingle(id);
            if (u.Password == dto.OldPassword.ToMD5())
            {
                u.Password = dto.Password.ToMD5();
                Unit.AuthUserRepository.Update(u);
                return Unit.SaveChanges();
            }
            else
            {
                return new SubmitResult(400, Unit.TranslateIfMobile("incorrect_password"));
            }
        }

        public virtual SubmitResult ResetPassword_SendMail(string email, string hostAddress)
        {
            var submitResult = new SubmitResult();

            var randomToken = Utils.RandomAlphabet(10, CharType.Small).ToMD5();
            var userId = Unit.AuthUserRepository.GetSingleValue(d => d.Id, d => d.Email == email);
            if (userId == 0)
            {
                throw new CodeShellHttpException(HttpStatusCode.NotFound, "Email Not Found");
            }

            ResetUserTokens[userId] = randomToken;
            var encrptedToken = Shell.Encryptor.Encrypt(randomToken);
            //string code = AsgaShell.GetTenantCode(AsgaShell.CurrentTenant);
            //string body = string.Format(hostAddress + "/" + code + "/Reset?userId={0}&token={1}", userId, encrptedToken);
            //AsgaEmailService.SendEmail(email, "SMEH Reset Password", body);
            submitResult.Code = 0;

            return submitResult;
        }

        public virtual SubmitResult ResetPassword(ResetDTO dto)
        {
            var submitResult = new SubmitResult();
            try
            {
                if (ResetUserTokens.TryGetValue(dto.UserId, out string existing))
                {
                    var token = Shell.Encryptor.Decrypt(dto.Token);
                    if (token == existing)
                    {
                        var user = Unit.AuthUserRepository.FindSingle(dto.UserId);
                        user.Password = dto.Password.ToMD5();
                        Unit.AuthUserRepository.Update(user);
                        Unit.SaveChanges();
                    }
                }
                submitResult.Code = 0;
            }
            catch// (Exception e)
            {
                submitResult.Code = 1;
            }
            return submitResult;
        }

        public virtual SubmitResult CheckIsUserExist(User dto)
        {
            var u = Unit.AuthUserRepository.FindSingle(s => s.LogonName.Equals(dto.LogonName));
            if (u != null)
            {
                return new SubmitResult(400, "exist_user");
            }
            else
            {
                return new SubmitResult(0, "");
            }
        }

        public override EditingDTO<User> GetSingleEditingDTO(object id)
        {
            var u = GetSingle(id);
            if (u != null)
            {
                u.PhotoFile = new CodeShellCore.Files.TmpFileData { Name = u.Photo?.GetAfterLast("/"), Url = u.Photo };
            }
            return new EditingDTO<User>
            {
                Id = u.Id,
                Entity = u
            };
        }


        public virtual string GetRoleNameById(long id)
        {
            return Unit.RoleRepository.GetValue(id, x => x.Name) ?? "";
        }

        public virtual Role GetUserRoleFromDb(long id)
        {
            var role = Unit.RoleRepository.FindSingle(r => r.UserRoles.Select(d => d.UserId).Contains(id));

            return role;
        }

        public virtual List<User> GetUsersByRoleId(long Roleid)
        {
            var xx = Unit.UserRoleRepository.Find(x => x.RoleId == Roleid);
            return Repository.Find(x => xx.Any(y => y.UserId == x.Id))?.ToList() ?? new List<User>();
        }

    }
}
