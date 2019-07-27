using Asga.Common.Data;
using Asga.Data;
using Asga.Security;
using CodeShellCore.Data.Services;
using CodeShellCore.DependencyInjection;
using CodeShellCore.Security.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Asga.Common
{
    public static class DependencyExtensions
    {
        public static void AddAsga(this IServiceCollection coll)
        {
            coll.AddScoped<CurrentTenant>();
            coll.AddTransient<IAuthorizationService, AuthorizationService>();
            coll.AddTransient(typeof(IEntityService<>), typeof(EntityService<>));
            coll.AddGenericRepository(typeof(AsgaRepository<,>));
            coll.AddTransient<IPermissionCacheService, PermissionCacheService>();
        }
    }
}
