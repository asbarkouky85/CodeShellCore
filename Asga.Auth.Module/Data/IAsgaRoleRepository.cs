using System;
using CodeShellCore.Data;

namespace Asga.Auth.Data
{
    public interface IAsgaRoleRepository : IRepository<Role>
    {
        DateTime GetLastUpdateDate();
        Role GetUserSpecializedRole(long id);
    }
}
