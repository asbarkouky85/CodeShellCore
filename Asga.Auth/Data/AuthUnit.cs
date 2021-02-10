using CodeShellCore.Data;
using CodeShellCore.Security;
using Asga.Auth.Data;
using Asga.Data;
using Asga.Security;
using Asga.Common.Data;
using System;

namespace Asga.Auth.Data
{
    public class AuthUnit : AsgaUnitBase<AuthContext>, IRoleBasedSecurityUnit, IAuthUnit
    {
        public AuthUnit(IServiceProvider acc) : base(acc)
        {
        }
        protected override Type GenericRepositoryType => typeof(AsgaRepository<,>);

        public IAsgaRoleRepository RoleRepository { get { return GetRepository<RoleRepository>(); } }
        public IAuthUserRepository AuthUserRepository { get { return GetRepository<UserRepository>(); } }
        public IResourceRepository ResourceRepository { get { return GetRepository<IResourceRepository>(); } }
        public IRoleRepository SecurityRoleRepository { get { return GetRepository<IRoleRepository>(); } }
        public IUserRepository UserRepository { get { return GetRepository<IUserRepository>(); ; } }
        public IUsersEntityLinkRepository UsersEntityLinkRepository => GetRepository<IUsersEntityLinkRepository>();


        public IRepository<App> AppRepository { get { return GetRepositoryFor<App>(); } }
        public IRepository<Domain> DomainRepository { get { return GetRepositoryFor<Domain>(); } }
        public IRepository<Resource> ResourceAsgaRepository { get { return GetRepositoryFor<Resource>(); } }
        public IRepository<ResourceAction> ResourceActionRepository { get { return GetRepositoryFor<ResourceAction>(); } }
        public IRepository<RoleResource> RoleResourceRepository { get { return GetRepositoryFor<RoleResource>(); } }
        public IRepository<RoleResourceAction> RoleResourceActionRepository { get { return GetRepositoryFor<RoleResourceAction>(); } }
        public IRepository<Tenant> TenantRepository { get { return GetRepositoryFor<Tenant>(); } }
        public IRepository<UserRole> UserRoleRepository { get { return GetRepositoryFor<UserRole>(); } }

        
    }
}
