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
        IUser GetByCredentials(string name, string password,bool forUi=true);
        IUser GetByUserId(object c, bool forUi = true);
        IUser GetByName(string userName, bool forUi = true);
        RegisterResult AddUser(IRegisterModel model);
        bool NameExists(string logonName);
        bool EmailExists(string email);
        void ResetUserPassword(ChangePasswordDTO dto);
    }
}
