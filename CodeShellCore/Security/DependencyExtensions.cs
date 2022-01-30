using CodeShellCore.Caching;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authentication.Internal;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Security
{
    public static class DependencyExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coll"></param>
        /// <param name="authenticatedOnly">if false all users are allowed to all apis, true means that all logged in users will be allowed to pass</param>
        public static void AddCodeShellSecurity(this IServiceCollection coll, bool authenticatedOnly = false)
        {
            coll.AddTransient<IUserDataService, UserDataService>();
            coll.AddTransient<ICacheProvider, MemoryCacheProvider>();
            if (!authenticatedOnly)
                coll.AddTransient<IAuthorizationService, AuthorizationService>();
            else
                coll.AddTransient<IAuthorizationService, AuthenticatedOnlyAuthorizationService>();

            coll.AddTransient<IAuthenticationService, PermissableAuthenticationService>();
        }

        public static void AddCodeShellSecurity<TUnit, TSession>(this IServiceCollection coll) where TUnit : class, ISecurityUnit where TSession : class, ISessionManager
        {
            coll.AddCodeShellSecurity();
            coll.AddTransient<IUserDataService, DbUserDataService>();
            coll.AddTransient<ISessionManager, TSession>();
            coll.AddScoped<ISecurityUnit, TUnit>();

        }

        public static void AddCodeShellSecurity<TUnit>(this IServiceCollection coll, string userId) where TUnit : class, ISecurityUnit
        {
            coll.AddCodeShellSecurity();
            coll.AddTransient<IUserDataService, DbUserDataService>();
            coll.AddTransient<ISessionManager>(d => new TestSessionManager(userId, d));
            coll.AddScoped<ISecurityUnit, TUnit>();

        }
    }
}
