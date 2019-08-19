using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Data;
using Asga.Auth.Data;
using Asga.Auth.Services;
using Asga.Services;
using CodeShellCore.Services;
using Asga.Common.Services;
using Asga.Common.Data;
using Asga.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security;

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
            coll.AddCollectionUnitOfWork<AuthUnit>();
            coll.AddScoped<ISecurityUnit>(d => d.GetService<AuthUnit>());
            coll.AddScoped<IRoleBasedSecurityUnit>(d => d.GetService<AuthUnit>());

            coll.AddTransient<IUserRepository, UserRepository>();
            coll.AddTransient<IRoleRepository, RoleRepository>();
            coll.AddTransient<IResourceRepository, ResourceRepository>();

            coll.AddTransient<AsgaCollectionService>();
            coll.AddRepositoryFor<Domain, DomainRepository>();
            coll.AddRepositoryFor<Resource, ResourceRepository>();
            coll.AddRepositoryFor<Role, RoleRepository>();
            coll.AddRepositoryFor<User, UserRepository>();

            coll.AddRepositoryFor<RoleResource, AsgaRepository<RoleResource, AuthContext>>();
            coll.AddRepositoryFor<RoleResourceAction, AsgaRepository<RoleResourceAction, AuthContext>>();
            coll.AddRepositoryFor<UserRole, AsgaRepository<UserRole, AuthContext>>();
            coll.AddRepositoryFor<DefaultRole, AsgaRepository<DefaultRole, AuthContext>>();
            coll.AddRepositoryFor<TenantAppUser, AsgaRepository<TenantAppUser, AuthContext>>();
            coll.AddRepositoryFor<UserParty, AsgaRepository<UserParty, AuthContext>>();

        }

        public static void AddAuthModule(this IServiceCollection coll, bool defaultModule = true)
        {

            coll.AddAuthData(defaultModule);

            coll.AddTransient<WriterService>();
            coll.AddTransient<AuthLookupService>();
            coll.AddScoped<CurrentTenant>();
            coll.AddTransient<AuthorizationService>();
            coll.AddHandlerFor<Role, RolesService>();
            coll.AddHandlerFor<User, UsersService>();
        }



        //public static void AuthConsumers(this IRabbitMqReceiveEndpointConfigurator e)
        //{

        //}
    }
}
