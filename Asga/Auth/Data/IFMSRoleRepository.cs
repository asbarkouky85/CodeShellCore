using System;
using Asga.Data;

namespace Asga.Auth.Data
{
    public interface IAsgaRoleRepository : IAsgaRepository<Role>
    {
        DateTime GetLastUpdateDate();
    }
}
