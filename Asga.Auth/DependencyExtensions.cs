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
using CodeShellCore.Web.Security;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Security.Authorization;

namespace Asga.Auth
{
    public static class DependencyExtensions
    {
        public static void AddAuthData(this IServiceCollection coll, bool asDefaultModule)
        {
            if (asDefaultModule)
            {
                coll.AddUnitOfWork<AuthUnit, IAsgaUnit>();
                coll.AddContext<AuthContext>();
            }
            else
            {
                coll.AddScoped<AuthUnit>();
                coll.AddScoped<IAsgaUnit, AuthUnit>();
                coll.AddScoped<AuthContext>();
            }
            coll.AddCollectionUnitOfWork<AuthUnit>();
            coll.AddTransient<ISecurityUnit, AuthUnit>();
            coll.AddTransient<IRoleBasedSecurityUnit, AuthUnit>();

            coll.AddTransient<AsgaCollectionService>();
            coll.AddRepositoryFor<Domain, DomainRepository>();
            coll.AddRepositoryFor<Resource, ResourceRepository>();
            coll.AddRepositoryFor<Role, RoleRepository>();
            coll.AddRepositoryFor<User, UserRepository>();
        }

        public static void AddAuthModule(this IServiceCollection coll, bool defaultModule = true)
        {
            coll.AddAuthData(defaultModule);
            coll.AddTransient<WriterService>();
            coll.AddTransient<AuthLookupService>();
            coll.AddScoped<CurrentTenant>();
            coll.AddTransient<IAuthenticationService, TokenAuthenticationService>();
            coll.AddSingleton<ISessionManager, TokenSessionManager>();
            coll.AddTransient<AuthorizationService>();
            coll.AddHandlerFor<Role, RolesService>();
            coll.AddHandlerFor<User, UsersService>();
            

        }



        //public static void AuthConsumers(this IRabbitMqReceiveEndpointConfigurator e)
        //{

        //}
    }
}
