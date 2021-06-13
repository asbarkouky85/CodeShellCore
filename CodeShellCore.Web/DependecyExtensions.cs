using CodeShellCore.Http.Pushing;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Services.Notifications;
using CodeShellCore.Web.Features;
using CodeShellCore.Web.Moldster.Configurator;
using CodeShellCore.Web.Notifiers;
using CodeShellCore.Web.Security;
using CodeShellCore.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using System;
using System.Diagnostics;
using System.Reflection;

namespace CodeShellCore.Web
{
    public static class DependecyExtensions
    {

        public static void AddSignalRHub<TContract, THub>(this IServiceCollection coll) where THub : Hub<TContract> where TContract : class
        {
            coll.AddTransient<IMessagePusher<TContract>, SignalRNotifier<THub, TContract>>();
        }

        public static void AddTokenSecurity(this IServiceCollection coll, AuthorizationType type)
        {
            coll.AddCodeShellSecurity(type);
            coll.AddTransient<ISessionManager, TokenSessionManager>();
            coll.AddTransient<IAuthenticationService, TokenAuthenticationService>();
        }

        public static void AddTokenSecurity<TUnit>(this IServiceCollection coll, AuthorizationType type = AuthorizationType.AuthorizeAuthenticated) where TUnit : class, ISecurityUnit
        {
            coll.AddCodeShellSecurity<TUnit, TokenSessionManager>(type);
            coll.AddTransient<IAuthenticationService, TokenAuthenticationService>();
        }

        public static void AddSOASecurity<TSessions>(this IServiceCollection coll)
            where TSessions : TokenSessionManager
        {
            coll.AddCodeShellSecurity();
            coll.AddTransient<ISessionManager, TSessions>();
        }

        public static void AddModsterSecurity(this IServiceCollection coll)
        {
            coll.AddCodeShellSecurity();
            coll.AddScoped<CurrentConfig>();
            coll.AddTransient<IAuthorizationService, UIAuthorizationService>();
            coll.AddTransient<IAuthenticationService, UIAuthenticationService>();
            coll.AddTransient<ISessionManager, UISessionManager>();
            coll.AddTransient<IPushingSessionManager, UISessionManager>();
            coll.AddTransient<IUserDataService, UIUserDataService>();

        }


        public static void AddRazorForConsole(this IServiceCollection coll)
        {
            coll.AddMvc();
            coll.AddTransient<ILoggerFactory, LoggerFactory>();
            coll.AddSingleton<IWebHostEnvironment, ConsoleHostingEnvironment>();
            coll.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            coll.AddSingleton<DiagnosticSource>(d => new DiagnosticListener("app"));
            coll.AddScoped<HttpContext, DefaultHttpContext>();

        }

        public static void ConfigureAddedServices(this IMvcBuilder mvc, string controllerNameSpace, Action<IFeatureConfiguration> configure)
        {
            var conf = new FeatureConfiguration();
            configure.Invoke(conf);

            mvc.ConfigureApplicationPartManager(d =>
            {
                d.FeatureProviders.Add(new CustomizableFeatureProvider(controllerNameSpace, conf));
            });
        }

        public static void ConfigureBlockedControllers(this IMvcBuilder mvc, Action<ControllerBlockerOptions> configure)
        {
            var conf = new ControllerBlockerOptions();
            configure.Invoke(conf);

            mvc.ConfigureApplicationPartManager(d =>
            {
                d.FeatureProviders.Add(new ControllerBlockerFeatureProvider(conf));
            });
        }
    }
}
