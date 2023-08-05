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
using CodeShellCore.Extensions.DependencyInjection;

namespace Asga.Auth
{
    public static class AsgaAuthModule
    {
        public static void AddAuthData(this IServiceCollection coll, bool asDefaultModule, IConfiguration config)
        {

            coll.AddCodeshellDbContext<AuthContext>(asDefaultModule, config, "Auth");
            coll.AddAuthData(asDefaultModule);
        }

        public static void AddAuthData(this IServiceCollection coll, bool asDefaultModule, Action<DbContextOptionsBuilder> opts)
        {

            coll.AddDbContext<AuthContext>(opts);
            coll.AddAuthData(asDefaultModule);
        }

        public static void AddAuthData(this IServiceCollection coll, bool asDefaultModule)
        {
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

        public static void AddAsgaAuthModule(this IServiceCollection coll)
        {
            coll.AddCodeShellApplication();
            coll.AddTransient<IAuthLookupService, AuthLookupService>();
            coll.AddTransient<IAuthenticationMailService, AsgaAuthMailService>();
            coll.AddTransient<AuthorizationService>();
            coll.AddServiceFor<Role, RolesService, IRolesService>();
            coll.AddServiceFor<User, UsersService, IUsersService>();

            coll.AddDataSeeders(typeof(IAuthUnit).Assembly);
        }

        public static void AddAsgaAuthModule(this IServiceCollection coll, IConfiguration config, bool defaultModule = true)
        {
            coll.AddAuthData(defaultModule, config);
            coll.AddAsgaAuthModule();
        }

        public static void AddAsgaAuthModule(this IServiceCollection coll, bool defaultModule, Action<DbContextOptionsBuilder> builder)
        {
            coll.AddAuthData(defaultModule, builder);
            coll.AddAsgaAuthModule();
        }

        public static void MigrateAuthDb(this IServiceProvider prov, bool seed = true)
        {
            

        }
    }
}
