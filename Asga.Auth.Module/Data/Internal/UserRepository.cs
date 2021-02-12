using System.Linq;

using CodeShellCore.Security;
using CodeShellCore.Text;
using CodeShellCore.Security.Authentication;

using Asga.Security;
using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Files;
using Asga.Auth.Dto;
using CodeShellCore.Linq;

namespace Asga.Auth.Data
{
    public class UserRepository : AsgaRepository<User, AuthContext>, IAuthUserRepository
    {
        public UserRepository(AuthContext con, ICollectionConfigService service, IUserAccessor acc) : base(con, service, acc)
        {
        }

        protected virtual IQueryable<UserListDTO> QueryUserListDTO(IQueryable<User> q = null)
        {
            q = q ?? Loader;
            return q.Select(d => new UserListDTO
            {
                Id = d.Id,
                Name = d.Name,
                LogonName = d.LogonName,
                AppName = d.AppId.HasValue ? d.App.Name : null,
                CreatedBy = d.CreatedBy,
                CreatedOn = d.CreatedOn,
                Email = d.Email,
                PersonId = d.PersonId,
                Mobile = d.Mobile,
                TenantName = d.Tenant.Name,
                GenderName = d.Gender.HasValue ? (d.Gender.Value ? "Male" : "Female") : null,
                BirthDate = d.BirthDate
            });
        }

        protected virtual IQueryable<UserDTO> QueryUserDTO(IQueryable<User> q = null)
        {
            q = q ?? Loader;
            string code = "";

            return q.Select(d => new UserDTO
            {
                Id = d.Id,
                UserId = d.Id.ToString(),
                LogonName = d.LogonName,
                Name = d.Name,
                TenantId = 0,
                TenantCode = code,
                UserTypeInt = d.UserType,
                PersonId = d.PersonId,
                Apps = d.AppId == null ? new string[0] : new string[] { d.App.Name },
                Roles = d.UserRoles.Select(e => e.RoleId.ToString()).ToList(),
                App = d.AppId != null ? d.App.Name : null,
                Photo = d.Photo
            });
        }

        public virtual IUser GetByCredentials(string name, string password, bool forUi = true)
        {
            string pass = password.ToMD5();
            return QueryUserDTO(Loader.Where(d => d.LogonName == name && d.Password == pass)).FirstOrDefault();
        }

        public virtual IUser GetByUserId(string c, bool forUi = true)
        {
            long id = 0;
            if (c is string)
                long.TryParse(c as string, out id);

            var dto = QueryUserDTO(Loader.Where(d => d.Id == id)).FirstOrDefault();
            
            return dto;
        }

        public virtual IUser GetByName(string userName, bool forUi = true)
        {
            var dto = QueryUserDTO(Loader.Where(d => d.LogonName == userName && (!d.IsDeleted))).FirstOrDefault();
            return dto;
        }

        public virtual RegisterResult AddUser(IRegisterModel model)
        {
            model.Password = model.Password.ToMD5();
            Saver.Add((User)model);
            return new RegisterResult { Success = true };
        }

        public override void Update(User obj)
        {
            if (obj.PhotoFile?.TmpPath != null)
            {
                obj.Photo = obj.PhotoFile.SaveFile("user_photos");
            }
            base.Update(obj);
        }

        public override void Add(User obj)
        {
            if (obj.PhotoFile?.TmpPath != null)
            {
                obj.Photo = obj.PhotoFile.SaveFile("user_photos");
            }
            base.Add(obj);
        }

        public virtual bool NameExists(string logonName)
        {
            return Loader.Any(d => d.LogonName == logonName);
        }

        public bool EmailExists(string email)
        {
            return Loader.Any(d => d.Email == email);
        }

        public void ResetUserPassword(ChangePasswordDTO dto)
        {
            User u = Loader.FirstOrDefault(d => d.Email == dto.Email);
            if (u != null)
            {
                u.Password = dto.Password.ToMD5();
                Update(u);
            }

        }

        public virtual LoadResult<UserListDTO> GetUserListDTOs(LoadOptions opt, string collectionId = null)
        {
            var q = collectionId == null ? Loader : QueryCollection(collectionId);
            return QueryUserListDTO(q).LoadWith(opt.GetOptionsFor<UserListDTO>());
        }
    }
}
