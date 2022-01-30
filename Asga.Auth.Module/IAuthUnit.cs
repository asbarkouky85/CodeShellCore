using Asga.Auth.Data;
using CodeShellCore.Data;
using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Auth
{
    public interface IAuthUnit : ISecurityUnit
    {
        IAuthUserRepository AuthUserRepository { get; }
        IRoleRepository SecurityRoleRepository { get; }
        IAsgaRoleRepository RoleRepository { get; }
        

        IRepository<RoleResource> RoleResourceRepository { get; }
        IRepository<RoleResourceAction> RoleResourceActionRepository { get; }
        IRepository<UserRole> UserRoleRepository { get; }

        IAsgaResourceRepository AsgaResourceRepository { get; }

        IRepository<App> AppRepository { get; }
        IRepository<Tenant> TenantRepository { get; }

        IRepository<Domain> DomainRepository { get; }

        IRepository<ResourceAction> ResourceActionRepository { get; }
    }
}
