using CodeShellCore.Data;
using CodeShellCore.Security;
using Asga.Auth.Data;
using Asga.Data;
using Asga.Security;
using Asga.Common.Data;

namespace Asga.Auth
{
    public class AuthUnit : AsgaUnitBase<AuthContext>, IRoleBasedSecurityUnit
    {
        public AuthUnit( IUserAccessor acc) : base( acc)
        {
        }

        public IUserRepository UserRepository { get { return GetRepository<IUserRepository>(); ; } }
        public IAuthUserRepository AuthUserRepository { get { return GetRepository<UserRepository>(); } }
        public IResourceRepository ResourceRepository { get { return GetRepository<IResourceRepository>(); } }
        public IRoleRepository SecurityRoleRepository { get { return GetRepository<IRoleRepository>(); } }
        public IAsgaRoleRepository RoleRepository { get { return GetRepository<RoleRepository>(); } }

        public IRepository<RoleResource> RoleResourceRepository { get { return GetRepositoryFor<RoleResource>(); } }
        public IRepository<RoleResourceAction> RoleResourceActionRepository { get { return GetRepositoryFor<RoleResourceAction>(); } }
        public IRepository<UserRole> UserRoleRepository { get { return GetRepositoryFor<UserRole>(); } }
        public IRepository<UserParty> UserPartyRepository { get { return GetRepositoryFor<UserParty>(); } }
        public IRepository<Resource> ResourceAsgaRepository { get { return GetRepositoryFor<Resource>(); } }
        public IRepository<TenantDomain> TenantDomainRepository { get { return GetRepositoryFor<TenantDomain>(); } }
        public IRepository<TenantApp> TenantAppRepository { get { return GetRepositoryFor<TenantApp>(); } }
        public IRepository<Tenant> TenantRepository { get { return GetRepositoryFor<Tenant>(); } }
        public IRepository<TenantAppUser> TenantAppUsersRepository { get { return GetRepositoryFor<TenantAppUser>(); } }
        public IRepository<Domain> DomainRepository { get { return GetRepositoryFor<Domain>(); } }
        public IRepository<DefaultRole> DefaultRoleRepository { get { return GetRepositoryFor<DefaultRole>(); } }
        public IRepository<ResourceAction> ResourceActionRepository { get { return GetRepositoryFor<ResourceAction>(); } }
    }
}
