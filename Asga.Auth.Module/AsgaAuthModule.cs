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
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using Asga.Auth.Seeding;

namespace Asga.Auth
{
    public static class AsgaAuthModule
    {
        public static void AddAuthData(this IServiceCollection coll, bool asDefaultModule, IConfiguration config = null)
        {

            coll.AddCodeshellDbContext<AuthContext>(asDefaultModule, config, "Auth");
            coll.AddUnitOfWork<AuthUnit, IAuthUnit>(asDefaultModule);

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

        public static void AddAsgaAuthModule(this IServiceCollection coll, IConfiguration config, bool defaultModule = true)
        {
            coll.AddAuthData(defaultModule, config);
            coll.AddCodeShellApplication();
            coll.AddTransient<IAuthLookupService, AuthLookupService>();
            coll.AddTransient<IAuthenticationMailService, AsgaAuthMailService>();
            coll.AddTransient<AuthorizationService>();
            coll.AddServiceFor<Role, RolesService, IRolesService>();
            coll.AddServiceFor<User, UsersService, IUsersService>();
            
            coll.AddDataSeeders<AuthContext>(e =>
            {
                e.AddSeeder<AuthDataSeeder>();
            });
        }

        public static void MigrateAuthDb(this IServiceProvider prov,bool seed=true)
        {
            prov.MigrateContext<AuthContext>(seed);
            
        }
    }
}
