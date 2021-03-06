﻿using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using CodeShellCore.Web.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using CodeShellCore.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeShellCore.Web
{
    public abstract class WebShell : Shell
    {
        private string _appRoot;
        private IServiceProvider _appProvider;

        public static string AppRootUrl { get { return ((WebShell)App).urlRoot; } }

        protected virtual bool UseHealthChecks => false;
        protected virtual string urlRoot { get { return "~"; } }
        protected override string appRoot { get { return _appRoot; } }
        protected override string publicRelativePath => "wwwroot";
        protected virtual bool IsSpa => false;

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

        protected virtual void HandleSystemErrors(HttpContext cont, Exception ex)
        {
            var res = cont.HandleRequestError(ex);
            cont.Response.ContentType = "application/json";
            cont.Response.WriteAsync(res.ToJson());
        }

        public virtual void ConfigureHttp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _appRoot = env.ContentRootPath;

            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = cont =>
                {
                    return Task.Run(() =>
                    {
                        var feat = cont.Features.Get<IExceptionHandlerPathFeature>();
                        HandleSystemErrors(cont, feat.Error);
                    });
                }
            });

            app.UseMvc(d =>
            {
                RegisterRoutes(d);
            });

            if (IsSpa)
                app.Use(FallbackMiddlewareHandler);

            if (UseHealthChecks)
            {
                app.UseHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = reg =>
                    {
                        return true;
                    },
                    ResponseWriter = (d, e) =>
                    {
                        return d.Response.WriteAsync(e.ToJson());
                    }
                });
            }

            _appProvider = app.ApplicationServices;
        }

        public virtual void AddMvcFeatures(IMvcBuilder mvc)
        {

        }

        protected virtual async Task FallbackMiddlewareHandler(HttpContext context, Func<Task> next)
        {
            var file = System.IO.File.ReadAllText("wwwroot/index.html");
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync(file);
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

            coll.Configure<MvcOptions>(r =>
            {
                r.EnableEndpointRouting = false;
            });
            var mvc = coll.AddMvc();
            if (UseHealthChecks)
            {
                coll.AddHealthChecks();
            }
            AddMvcFeatures(mvc);
            mvc.AddNewtonsoftJson(e => e.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local);
            coll.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            coll.AddTransient<IFileUploadService, FileService>();
        }

        protected override IConfigurationSection getConfig(string key)
        {
            return Configuration.GetSection(key);
        }


    }
}
