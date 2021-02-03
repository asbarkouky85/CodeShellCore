using Asga.Auth;
using Asga.Auth.Data;
using Asga.Web.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Web;
using CodeShellCore.Web.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Auth
{
   public static class DependencyInjectionExtensions
    {
        public static void AddAsgaWeb(this IServiceCollection coll)
        {
            coll.AddTokenSecurity<AuthUnit>();
            coll.AddTransient<IAuthorizationService, AsgaAuthorizationService>();
            coll.AddTransient<IAuthenticationService, AsgaAuthenticationService>();
        }
    }
}
