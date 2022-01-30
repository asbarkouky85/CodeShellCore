using System.Linq;

using CodeShellCore.Security;
using CodeShellCore.Text;
using CodeShellCore.Security.Authentication;

using Asga.Security;
using CodeShellCore.Data.ConfiguredCollections;

namespace Asga.Auth.Data
{
    public class UserRepository : AsgaRepository<User, AuthContext>, IAuthUserRepository
    {
        public UserRepository(AuthContext con, ICollectionConfigService service, IUserAccessor acc) : base(con, service, acc)
        {
        }

        internal protected virtual IQueryable<UserDTO> QueryUserDTO(IQueryable<User> q = null)
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

            return QueryUserDTO(Loader.Where(d => d.Id == id)).FirstOrDefault();
        }

        public virtual IUser GetByName(string userName, bool forUi = true)
        {
            return QueryUserDTO(Loader.Where(d => d.LogonName == userName && (!d.IsDeleted))).FirstOrDefault();
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
    }
}
