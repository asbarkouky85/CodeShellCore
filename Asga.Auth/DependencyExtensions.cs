using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.DependencyInjection;
using CodeShellCore.Data;
using CodeShellCore.Services;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security;

using Asga.Auth.Data;
using Asga.Auth.Services;
using Asga.Common.Services;
using Asga.Common.Data;
using Asga.Security;

namespace Asga.Auth
{
    public static class DependencyExtensions
    {
        public static void AddAuthData(this IServiceCollection coll, bool asDefaultModule)
        {
            if (asDefaultModule)
            {
                coll.AddUnitOfWork<AuthUnit>();
                coll.AddContext<AuthContext>();
            }
            else
            {
                coll.AddScoped<AuthUnit>();
                coll.AddScoped<AuthContext>();
            }

            coll.AddScoped<ISecurityUnit>(d => d.GetService<AuthUnit>());
            coll.AddScoped<IRoleBasedSecurityUnit>(d => d.GetService<AuthUnit>());

            coll.AddTransient<IUserRepository, UserRepository>();
            coll.AddTransient<IRoleRepository, RoleRepository>();
            coll.AddTransient<IResourceRepository, ResourceRepository>();
            coll.AddTransient<IUsersEntityLinkRepository, UserEntityLinkRepository>();

            coll.AddRepositoryFor<Domain, DomainRepository>();
            coll.AddRepositoryFor<Resource, ResourceRepository>();
            coll.AddRepositoryFor<Role, RoleRepository>();
            coll.AddRepositoryFor<User, UserRepository,IAuthUserRepository>();
            coll.AddRepositoryFor<UserEntityLink, UserEntityLinkRepository>();

            coll.AddCollectionUnitOfWork<AuthUnit>();
            coll.AddTransient<AsgaCollectionService>();
            coll.AddTransient(typeof(AsgaRepository<,>));

        }

        public static void AddAuthModule(this IServiceCollection coll, bool defaultModule = true)
        {

            coll.AddAuthData(defaultModule);

            coll.AddTransient<WriterService>();
            coll.AddTransient<AuthLookupService>();
            coll.AddScoped<CurrentTenant>();
            coll.AddTransient<AuthorizationService>();
            coll.AddServiceFor<Role, RolesService, IRolesService>();
            coll.AddServiceFor<User, UsersService, IUsersService>();
        }
    }
}
