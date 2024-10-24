using CodeShellCore.Caching;
using CodeShellCore.Files.Uploads;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security
{
    public static class ApplicationSecurityExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coll"></param>
        /// <param name="authenticatedOnly">if false all users are allowed to all apis, true means that all logged in users will be allowed to pass</param>
        public static void AddCodeShellAuthorization(this IServiceCollection coll, AuthorizationType type = AuthorizationType.AuthorizeAuthenticated)
        {

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

        }

        public static void AddCodeShellSecurity(this IServiceCollection coll, string userId)
        {
            coll.AddCodeShellAuthorization();
            coll.AddTransient<ISessionManager>(d => new TestSessionManager(userId, d));
        }
    }
}
