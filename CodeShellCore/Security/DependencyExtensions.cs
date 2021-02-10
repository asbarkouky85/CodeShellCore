using CodeShellCore.Caching;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Security
{
    public static class DependencyExtensions
    {
        public static void AddCodeShellSecurity(this IServiceCollection coll)
        {
            coll.AddTransient<IUserDataService, UserDataService>();
            coll.AddTransient<ICacheProvider, MemoryCacheProvider>();
            coll.AddTransient<IAuthorizationService, AuthorizationService>();
            coll.AddTransient<IAuthenticationService, DefaultAuthenticationService>();
        }

        public static void AddCodeShellSecurity<TUnit, TSession>(this IServiceCollection coll) where TUnit : class, ISecurityUnit where TSession : class, ISessionManager
        {
            coll.AddCodeShellSecurity();
            coll.AddTransient<IUserDataService, DbUserDataService>();
            coll.AddTransient<ISessionManager, TSession>();
            coll.AddScoped<ISecurityUnit, TUnit>();

        }

        public static void AddCodeShellSecurity<TUnit>(this IServiceCollection coll, long userId) where TUnit : class, ISecurityUnit
        {
            coll.AddCodeShellSecurity();
            coll.AddTransient<IUserDataService, DbUserDataService>();
            coll.AddTransient<ISessionManager>(d => new TestSessionManager(userId));
            coll.AddScoped<ISecurityUnit, TUnit>();

        }
    }
}
