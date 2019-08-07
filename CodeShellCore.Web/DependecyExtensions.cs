using CodeShellCore.Caching;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Web.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Web
{
    public static class DependecyExtensions
    {
        public static void AddTokenSecurity<TUnit>(this IServiceCollection coll) where TUnit : class, ISecurityUnit
        {
            coll.AddCodeShellSecurity<TUnit, TokenSessionManager>();
            coll.AddTransient<IAuthenticationService, TokenAuthenticationService>();
        }
    }
}
