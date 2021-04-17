using Asga.Auth.Dto;
using CodeShellCore.Data;
using CodeShellCore.Linq;
using CodeShellCore.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Auth.Data
{
    public interface IAuthUserRepository : IRepository<User>, IUserRepository
    {
        LoadResult<UserListDTO> GetUserListDTOs(LoadOptions opt, string collectionId = null);
    }
}
