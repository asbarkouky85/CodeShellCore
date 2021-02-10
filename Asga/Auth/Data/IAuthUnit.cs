using CodeShellCore.Data;
using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Auth.Data
{
    public interface IAuthUnit : ICollectionUnitOfWork, ISecurityUnit
    {
        IAuthUserRepository AuthUserRepository { get; }
        IRoleRepository SecurityRoleRepository { get; }
        IAsgaRoleRepository RoleRepository { get; }
        

        IRepository<RoleResource> RoleResourceRepository { get; }
        IRepository<RoleResourceAction> RoleResourceActionRepository { get; }
        IRepository<UserRole> UserRoleRepository { get; }

        IRepository<Resource> ResourceAsgaRepository { get; }

        IRepository<App> AppRepository { get; }
        IRepository<Tenant> TenantRepository { get; }

        IRepository<Domain> DomainRepository { get; }

        IRepository<ResourceAction> ResourceActionRepository { get; }
    }
}
