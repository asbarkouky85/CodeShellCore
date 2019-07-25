using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.Text.Localization;
using CodeShellCore.Security.Authorization;
using CodeShellCore.Services;
using CodeShellCore.MQ.Events;
using CodeShellCore.MQ;
using CodeShellCore.Text.TextProviders;
using CodeShellCore.Data.Services;
using CodeShellCore.Web.Services;

namespace CodeShellCore.Web
{
    public abstract class WebShell : Shell
    {
        private string _appRoot;
        private IServiceProvider _appProvider;

        public static string AppRootUrl { get { return ((WebShell)App).urlRoot; } }

        protected virtual string urlRoot { get { return "~"; } }
        protected override string appRoot { get { return _appRoot; } }
        protected override string publicRelativePath => "wwwroot";

        protected IConfiguration Configuration;

        protected override IServiceProvider _scopedProvider
        {
            get
            {
                var acc = RootInjector.GetService<IHttpContextAccessor>();

                if (acc.HttpContext == null)
                    return null;

                return acc.HttpContext.RequestServices;
            }
        }

        protected override IServiceProvider rootProvider { get { return _appProvider; } }
        

        public WebShell(IConfiguration config)
        {
            Configuration = config;
        }

        public virtual void ConfigureHttp(IApplicationBuilder app, IHostingEnvironment env)
        {
            _appRoot = env.ContentRootPath;
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseMvc(d => RegisterRoutes(d));
            _appProvider = app.ApplicationServices;
        }

        public virtual void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute(
                name: "apiAction",
                template: "apiAction/{controller}/{action}/{id?}"
                );

            routeBuilder.MapRoute(
                name: "api",
                template: "api/{controller}/{id?}"
                );

            routeBuilder.MapRoute(
                    name: "mvc",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" }
                    );
        }

        public override void RegisterServices(IServiceCollection coll)
        {
            base.RegisterServices(coll);
            coll.AddMvc().AddJsonOptions(e=> e.SerializerSettings.DateTimeZoneHandling=Newtonsoft.Json.DateTimeZoneHandling.Local);
            coll.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            coll.AddSingleton<AuthorizationService>();
            coll.AddTransient(typeof(IEntityService<>), typeof(EntityService<>));
            coll.AddTransient<FileService>();
            coll.AddScoped<Language>();
            if (useLocalization)
                coll.AddSingleton<ILocaleTextProvider, ResxTextProvider>();
        }

        protected override IConfigurationSection getConfig(string key)
        {
            return Configuration.GetSection(key);
        }


    }
}
