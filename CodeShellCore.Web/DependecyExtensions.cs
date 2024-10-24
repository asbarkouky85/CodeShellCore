using CodeShellCore.Http.Pushing;
using CodeShellCore.Security;
using CodeShellCore.Security.Authentication;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Web.Features;
using CodeShellCore.Web.Moldster.Configurator;
using CodeShellCore.Web.Proxy;
using CodeShellCore.Web.Security;
using CodeShellCore.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using System;
using System.Diagnostics;

namespace CodeShellCore.Web
{
    public static class DependecyExtensions
    {
        public static void AddCodeShellApiDocumentation(this IServiceCollection coll)
        {
            coll.AddTransient<IProxyDocumentationService, ProxyDocumentationService>();
        }

        public static void AddCodeShellWebSecurity()
        {

        }
        public static void AddTokenSecurity(this IServiceCollection coll)
        {
            coll.AddTransient<ISessionManager, TokenSessionManager>();
        }

        public static void AddTokenSecurity(this IServiceCollection coll, AuthorizationType type = AuthorizationType.AuthorizeAuthenticated)
        {
            coll.AddTransient<ISessionManager, TokenSessionManager>();
        }

        public static void AddSOASecurity<TSessions>(this IServiceCollection coll)
            where TSessions : TokenSessionManager
        {
            coll.AddTransient<ISessionManager, TSessions>();
        }

        public static void AddModsterSecurity(this IServiceCollection coll)
        {
            coll.AddScoped<CurrentConfig>();

            coll.AddTransient<ISessionManager, UISessionManager>();
            coll.AddTransient<IPushingSessionManager, UISessionManager>();

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
