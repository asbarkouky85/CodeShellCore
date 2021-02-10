using CodeShellCore.Data;
using CodeShellCore.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Auth.Data
{
    public interface IAuthUserRepository : IRepository<User>, IUserRepository
    {
    }
}
