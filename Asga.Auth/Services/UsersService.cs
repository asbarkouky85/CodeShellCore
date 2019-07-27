using Asga.Auth.Dto;
using Asga.Data;
using Asga.Services;
using CodeShellCore;
using CodeShellCore.Data.Helpers;
using CodeShellCore.Helpers;
using CodeShellCore.Linq;
using CodeShellCore.Security;
using CodeShellCore.Services.Http;
using CodeShellCore.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Asga.Auth.Services
{
    public class UsersService : AsgaEntityService<User>
    {
        private AuthUnit Unit;
        //private AsgaEmailService AsgaEmailService;
        private readonly IUserAccessor _user;
        private static Dictionary<long, string> ResetUserTokens = new Dictionary<long, string>();
        public UsersService(AuthUnit unit,
            //AsgaEmailService AsgaEmailService,
            IUserAccessor user) : base(unit)
        {
            Unit = unit;
            //AsgaEmailService = AsgaEmailService;
            _user = user;
        }

        #region Overrides

        public override User GetSingle(object id)
        {
            Unit.EnableJsonLoading();
            var ID = (long)id;
            var u = Repository.FindSingle(id);
            if (u == null)
            {
                return null;
                //throw new CodeShellHttpException(HttpStatusCode.NotFound, "id does not exist");
            }

            u.Roles = Unit.UserRoleRepository.Find(d => d.UserId == ID);
            u.Apps = Unit.TenantAppUsersRepository.Find(d => d.UserId == ID);
            u.Parties = Unit.UserPartyRepository.Find(d => d.UserId == ID);
            u.TenantAppUsers = null;

            u.Password = null;
            return u;
        }
        //public SubmitResult UpdateUserLogo(PartyLogoChange dto)
        //{
        //   var users= Repository.Find(u => dto.Data.Any(s => s == u.Id));

        //    foreach (var user in users)
        //    {
        //        user.CustomerLogo = dto.LogoPath;
        //    }
        //    return Unit.SaveChanges();
        //}

        public SubmitResult UpdateUserParties(UserPartiesChanged ev)
        {
            Unit.UserPartyRepository.Delete(d => d.UserId == ev.UserId);

            foreach (var id in ev.Parties)
            {
                Unit.UserPartyRepository.Add(new UserParty
                {
                    UserId = ev.UserId,
                    PartyId = id
                });
            }
            return Unit.SaveChanges();
        }

        public SubmitResult UpdateUserParties(long userId, long partyId)
        {
            Unit.UserPartyRepository.Add(new UserParty
            {
                UserId = userId,
                PartyId = partyId
            });
            return Unit.SaveChanges();
        }

        public override SubmitResult Create(User obj)
        {
            obj.Password = obj.Password?.ToMD5();

            if (obj.Role != null)
            {
                Unit.RoleRepository.Add(obj.Role);
                var ur = new UserRole
                {
                    Id = Utils.GenerateID(),
                };
                obj.UserRoles.Add(ur);
                obj.Role.UserRoles.Add(ur);
            }

            if (obj.RoleId != null)
            {
                obj.UserRoles.Add(new UserRole { RoleId = obj.RoleId.Value });
            }

            if (obj.Apps != null)
            {
                foreach (var ap in obj.Apps)
                {
                    ap.Id = Utils.GenerateID();
                    obj.TenantAppUsers.Add(ap);
                }
            }
            //check if user have not roles add default role  
            if (obj.Apps == null && obj.Role == null)
            {
                //get default role by user type
                var defaultRoles = Unit.DefaultRoleRepository.GetValues(d => new { d.RoleId, d.Role.TenantAppId }, c => c.UserType == obj.UserType);
                defaultRoles.ForEach(i =>
                {
                    //add User Role
                    var ur = new UserRole
                    {
                        Id = Utils.GenerateID(),
                        RoleId = i.RoleId ?? 0
                    };
                    obj.UserRoles.Add(ur);
                    //add Tenant App User
                    var appobj = new TenantAppUser
                    {
                        Id = Utils.GenerateID(),
                        TenantAppId = i.TenantAppId ?? 0,
                    };
                    obj.TenantAppUsers.Add(appobj);
                });
            }
            if (obj.Parties != null)
            {
                foreach (var ap in obj.Parties)
                {
                    ap.Id = Utils.GenerateID();
                    obj.UserParties.Add(ap);
                }
            }
            return base.Create(obj);
        }

        public override SubmitResult Update(User obj)
        {
            if (!string.IsNullOrEmpty(obj.Password))
            {
                obj.Password = obj.Password.ToMD5();
                Repository.Update(obj);
            }
            else
            {
                var u = Repository.FindSingle(obj.Id);
                u.AppendProperties(obj, true, new[] { "Password" });
                Repository.Update(u);
            }

            Unit.UserRoleRepository.ApplyChanges(obj.Roles);
            Unit.TenantAppUsersRepository.ApplyChanges(obj.Apps);
            Unit.UserPartyRepository.ApplyChanges(obj.Parties);
            if (obj.Role != null)
            {
                Unit.RoleResourceRepository.ApplyChanges(obj.Role.RoleResources);
                Unit.RoleResourceActionRepository.ApplyChanges(obj.Role.RoleResourceActions);
            }

            if (obj.RoleId != null)
            {
                Unit.UserRoleRepository.Delete(d => d.UserId == obj.Id);
                Unit.UserRoleRepository.Add(new UserRole
                {
                    RoleId = obj.RoleId.Value,
                    UserId = obj.Id
                });
            }
            //if role id == null means its delete so delete recored from user role table
            if(obj.RoleId == null)
            {
                Unit.UserRoleRepository.Delete(d => d.UserId == obj.Id);
            }

            return Unit.SaveChanges();
        }

        public override DeleteResult DeleteById(object prime)
        {
            return base.DeleteById(prime);
        }

        #endregion
        public User GetUserByUserName(string userName)
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


        public  long SoftDelete(long id ,bool isDeleted)
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

        public SimpleListModel<UserListModel> GetFilteredUsers(UsersFilterModel filter)
        {
            var filteredUsers = Repository.Find(x => x.UserType == 0  &&
            (string.IsNullOrEmpty(filter.Id) || x.Id.ToString() == filter.Id)
            && (string.IsNullOrEmpty(filter.Name) || x.FirstName.ToLower().Contains(filter.Name.ToLower()) || x.LastName.ToLower().Contains(filter.Name.ToLower()))
            && (string.IsNullOrEmpty(filter.Email) || x.Email.ToLower().Contains(filter.Email.ToLower()))
            && (string.IsNullOrEmpty(filter.UserName) || x.LogonName.ToLower().Contains(filter.UserName.ToLower()))
            && (filter.RoleId == 0 || x.UserRoles.Select(r => r.RoleId).Contains(filter.RoleId))
         );
            var count = filteredUsers.Count();

            var returnedList = filteredUsers.Select(x => new UserListModel
            {
                Id = x.Id,
                IsDeleted = x.IsDeleted,
                email = x.Email,
                LogonName = x.LogonName,
                Role = Unit.UserRoleRepository.Find(r => r.UserId == x.Id).Select(r => Unit.RoleRepository.
                FindSingle(rr => rr.Id == r.RoleId)).FirstOrDefault()?.Name ?? ""
            });
            return new SimpleListModel<UserListModel> { Count = count, List = returnedList };


        }

        public Role GetUserRole(long id)
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

        public SubmitResult ChangePassword(ChangePasswordDTO dto)
        {
            var u = Unit.AuthUserRepository.FindSingle(_user.User.UserId);
            if (u.Password == dto.OldPassword.ToMD5())
            {
                u.Password = dto.Password.ToMD5();
                return Unit.SaveChanges();
            }
            else
            {
                return new SubmitResult(400, "incorrect_password");
            }
        }

        public SubmitResult ResetPassword_SendMail(string email, string hostAddress)
        {
            var submitResult = new SubmitResult();

            var randomToken = Utils.RandomAlphabet(6);
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

        public SubmitResult ResetPassword(ResetDTO dto)
        {
            var submitResult = new SubmitResult();
            try
            {
                var token = Shell.Encryptor.Decrypt(dto.Token);
                if (ResetUserTokens.ContainsKey(dto.UserId) && ResetUserTokens.GetValueOrDefault(dto.UserId) == token)
                {
                    var user = Unit.AuthUserRepository.FindSingle(dto.UserId);
                    user.Password = dto.Password.ToMD5();
                    Unit.AuthUserRepository.Update(user);
                    Unit.SaveChanges();
                }
                submitResult.Code = 0;
            }
            catch// (Exception e)
            {
                submitResult.Code = 1;
            }
            return submitResult;
        }

        public SubmitResult CheckIsUserExist(User dto)
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
            return new EditingDTO<User>
            {
                Id = u.Id,
                Entity = u
            };
        }


        public string GetRoleNameById(long id)
        {
           return Unit.RoleRepository.GetValue(id, x => x.Name)??"";
        }
        
        public Role GetUserRoleFromDb(long id)
        {
            var role = Unit.RoleRepository.FindSingle(r => r.UserRoles.Select(d => d.UserId).Contains(id));
           
            return role;
        }

      public List<User> GetUsersByRoleId(long Roleid)
        {
      var xx=  Unit.UserRoleRepository.Find(x => x.RoleId == Roleid);
            return Repository.Find(x => xx.Any(y=>y.UserId==x.Id))?.ToList() ?? new List<User>();
        }

    }
}
