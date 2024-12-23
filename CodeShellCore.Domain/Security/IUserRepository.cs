using CodeShellCore.Data;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Security
{
    public interface IUserRepository : IRepository
    {
        Task<IUser> GetByCredentials(string name, string password, bool forUi = true);
        Task<IUser> GetByUserId(string c, bool forUi = true);
        Task<IUser> GetByName(string userName, bool forUi = true);
        Task<RegisterResult> AddUser(IRegisterModel model);
        Task<bool> NameExists(string logonName);
        Task<bool> EmailExists(string email);
        Task ResetUserPassword(ChangePasswordDTO dto);
    }
}
