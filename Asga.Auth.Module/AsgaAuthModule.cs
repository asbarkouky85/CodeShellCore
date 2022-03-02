using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.DependencyInjection;
using CodeShellCore.Data;
using CodeShellCore.Services;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security;

using Asga.Auth.Data;
using Asga.Auth.Services;
using Asga.Security;
using CodeShellCore.Data.ConfiguredCollections;
using CodeShellCore.Security.Authentication;

namespace Asga.Auth
{
    public static class AsgaAuthModule
    {
        public static void AddAuthData(this IServiceCollection coll, bool asDefaultModule)
        {
            if (asDefaultModule)
            {
                coll.AddUnitOfWork<AuthUnit, IAuthUnit>();
                coll.AddContext<AuthContext>();
            }
            else
            {
                coll.AddScoped<AuthUnit>();
                coll.AddScoped<AuthContext>();
                coll.AddScoped<IAuthUnit>(s => s.GetService<AuthUnit>());
            }

            coll.AddScoped<ISecurityUnit>(d => d.GetService<AuthUnit>());

            coll.AddTransient<IUserRepository, UserRepository>();
            coll.AddTransient<IRoleRepository, RoleRepository>();
            coll.AddTransient<IResourceRepository, ResourceRepository>();
            coll.AddTransient<IUsersEntityLinkRepository, UserEntityLinkRepository>();
            coll.AddTransient<IAuthUserRepository, UserRepository>();
            coll.AddTransient<IAsgaRoleRepository, RoleRepository>();

            coll.AddRepositoryFor<Domain, DomainRepository>();
            coll.AddRepositoryFor<Resource, ResourceRepository, IAsgaResourceRepository>();
            coll.AddRepositoryFor<Role, RoleRepository>();
            coll.AddRepositoryFor<User, UserRepository, IAuthUserRepository>();
            coll.AddRepositoryFor<UserEntityLink, UserEntityLinkRepository>();

            coll.AddTransient<ICollectionEFRepository<User, AuthContext>, AsgaRepository<User, AuthContext>>();
            coll.AddTransient<ICollectionEFRepository<Role, AuthContext>, AsgaRepository<Role, AuthContext>>();
            coll.AddTransient<ICollectionEFRepository<Resource, AuthContext>, AsgaRepository<Resource, AuthContext>>();

            coll.AddConfiguredCollections(typeof(AsgaRepository<,>));
            coll.AddTransient(typeof(AsgaRepository<,>));

        }

        public static void AddAuthModule(this IServiceCollection coll, bool defaultModule = true)
        {

            coll.AddAuthData(defaultModule);
            coll.AddTransient<IAuthLookupService, AuthLookupService>();
            coll.AddTransient<IAuthenticationMailService, AsgaAuthMailService>();
            coll.AddTransient<AuthorizationService>();
            coll.AddServiceFor<Role, RolesService, IRolesService>();
            coll.AddServiceFor<User, UsersService, IUsersService>();
        }
    }
}
