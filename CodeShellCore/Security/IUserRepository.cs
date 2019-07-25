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
        IUser GetByCredentials(string name, string password);
        
        IUser GetByUserId(object c);
        IUser GetByName(string userName);
        RegisterResult AddUser(IRegisterModel model);
        bool NameExists(string logonName);
    }
}
