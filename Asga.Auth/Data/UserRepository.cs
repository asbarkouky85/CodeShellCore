using System;
using System.Linq;
using System.Linq.Expressions;

using CodeShellCore.Security;
using CodeShellCore.Text;
using CodeShellCore.Security.Authentication;

using Asga.Security;
using Asga.Auth.Data;
using Asga.Data;
using Asga.Common.Services;
using Asga.Common;

namespace Asga.Auth.Data
{
    public class UserRepository : AsgaRepository<User, AuthContext>, IAuthUserRepository
    {
        CurrentTenant _tenant;
        public UserRepository(AuthContext con, AsgaCollectionService ser, CurrentTenant tenant) : base(con, ser)
        {
            _tenant = tenant;
        }

        internal protected virtual IQueryable<UserDTO> QueryUserDTO(IQueryable<User> q = null)
        {
            q = q ?? Loader;
            long ten = _tenant.TenantId;
            string code = AsgaShell.GetTenantCode(ten);
            string servic = "";
            return q.Select(d => new UserDTO
            {
                Id = d.Id,
                UserId = d.Id,
                LogonName = d.LogonName,
                Name = d.FirstName + " " + d.LastName,
                TenantId = d.TenantId ?? ten,
                TenantCode = code,
                UserTypeInt = d.UserType,
                PartyId = d.CustomerId,
                Apps = d.TenantAppUsers.Select(x => x.TenantApp.Name).ToList(),
                Customers = d.UserParties.Select(x => x.PartyId).ToList(),
                CustomerLogo = d.CustomerLogo == null ? null : servic + "/" + d.CustomerLogo
            });
        }

        public virtual IUser GetByCredentials(string name, string password)
        {
            string pass = password.ToMD5();
            return QueryUserDTO(Loader.Where(d => d.LogonName == name && d.Password == pass)).FirstOrDefault();
        }

        public virtual IUser GetByUserId(object c)
        {
            long id = 0;
            if (c is string)
                long.TryParse(c as string, out id);
            else
                id = (long)c;

            return Loader.Where(d => d.Id == id).Select(UserDTO.FromAuthUser).FirstOrDefault();
        }

        public virtual IUser GetByName(string userName)
        {
            return Loader.Where(d => d.LogonName == userName && (!d.IsDeleted)).Select(UserDTO.FromAuthUser).FirstOrDefault();
        }

        public virtual RegisterResult AddUser(IRegisterModel model)
        {
            model.Password = model.Password.ToMD5();
            Saver.Add((User)model);
            return new RegisterResult { Success = true };
        }

        public virtual bool NameExists(string logonName)
        {
            return Loader.Any(d => d.LogonName == logonName);
        }
    }
}
