using CodeShellCore.Data;
using CodeShellCore.Security;
using System;
using CodeShellCore.Data.EntityFramework;

namespace Asga.Auth.Data
{
    public class AuthUnit : UnitOfWork<AuthContext>, IRoleBasedSecurityUnit, IAuthUnit
    {
        public AuthUnit(IServiceProvider acc) : base(acc)
        {
        }
        protected override Type GenericRepositoryType => typeof(AsgaRepository<,>);
        protected override Type GenericCollectionRepositoryType => typeof(AsgaRepository<,>);
        protected override bool UseChangeColumns => true;
        protected override bool UseCollectionPermission => true;

        public IAsgaResourceRepository AsgaResourceRepository { get { return GetRepository<IAsgaResourceRepository>(); } }
        public IAsgaRoleRepository RoleRepository { get { return GetRepository<IAsgaRoleRepository>(); } }
        public IAuthUserRepository AuthUserRepository { get { return GetRepository<IAuthUserRepository>(); } }
        public IResourceRepository ResourceRepository { get { return GetRepository<IResourceRepository>(); } }
        public IRoleRepository SecurityRoleRepository { get { return GetRepository<IRoleRepository>(); } }
        public IUserRepository UserRepository { get { return GetRepository<IUserRepository>(); ; } }
        public IUsersEntityLinkRepository UsersEntityLinkRepository => GetRepository<IUsersEntityLinkRepository>();


        public IRepository<App> AppRepository { get { return GetRepositoryFor<App>(); } }
        public IRepository<Domain> DomainRepository { get { return GetRepositoryFor<Domain>(); } }
        public IRepository<ResourceAction> ResourceActionRepository { get { return GetRepositoryFor<ResourceAction>(); } }
        public IRepository<RoleResource> RoleResourceRepository { get { return GetRepositoryFor<RoleResource>(); } }
        public IRepository<RoleResourceAction> RoleResourceActionRepository { get { return GetRepositoryFor<RoleResourceAction>(); } }
        public IRepository<Tenant> TenantRepository { get { return GetRepositoryFor<Tenant>(); } }
        public IRepository<UserRole> UserRoleRepository { get { return GetRepositoryFor<UserRole>(); } }

        
    }
}
