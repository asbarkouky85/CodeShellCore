using CodeShellCore.Caching;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authentication.Internal;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security
{
  public static  class ApplicationSecurityExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coll"></param>
        /// <param name="authenticatedOnly">if false all users are allowed to all apis, true means that all logged in users will be allowed to pass</param>
        public static void AddCodeShellSecurity(this IServiceCollection coll, AuthorizationType type = AuthorizationType.AuthorizeAuthenticated)
        {
            coll.AddTransient<IUserDataService, UserDataService>();
            coll.AddTransient<ICacheProvider, MemoryCacheProvider>();
            switch (type)
            {
                case AuthorizationType.AuthorizeAll:
                    coll.AddTransient<IAuthorizationService, AuthorizationService>();
                    break;
                case AuthorizationType.AuthorizeAuthenticated:
                case AuthorizationType.AuthorizeWithApp:
                case AuthorizationType.AuthorizeWithResource:
                    coll.AddTransient<IAuthorizationService, AuthenticatedOnlyAuthorizationService>();
                    break;
                default:
                    coll.AddTransient<IAuthorizationService, AuthenticatedOnlyAuthorizationService>();
                    break;
            }

            coll.AddTransient<IAuthenticationService, PermissableAuthenticationService>();
        }

        public static void AddCodeShellSecurity<TUnit, TSession>(this IServiceCollection coll, AuthorizationType type = AuthorizationType.AuthorizeAuthenticated) where TUnit : class, ISecurityUnit where TSession : class, ISessionManager

        {
            coll.AddCodeShellSecurity(type);
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
