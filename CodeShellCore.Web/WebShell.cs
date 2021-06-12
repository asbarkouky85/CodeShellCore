using System;

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
        /// <summary>
        /// (Default : "~")
        /// </summary>
        protected virtual string urlRoot { get { return "~"; } }
        protected override string appRoot { get { return _appRoot; } }
        /// <summary>
        /// public folder (Default : 'wwwroot')
        /// </summary>
        protected override string publicRelativePath => "wwwroot";
        /// <summary>
        /// Adds a fallback middleware that uses <see cref="ISpaFallbackHandler.HandleRequestAsync(HttpContext)"/>
        /// </summary>
        protected virtual bool IsSpa => false;
        protected virtual bool UseCors => false;
        /// <summary>
        /// If AllowedOrigins not found in appsettings this will be used
        /// Default is : http://localhost,http://localhost:4200
        /// </summary>
        protected virtual string DefaultCorsOrigins => "http://localhost,http://localhost:4200";

        private IConfiguration _config;
        protected override IConfiguration Configuration => _config;

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

        /// <summary>
        /// Service provider created on <see cref="IConfigurationBuilder.Build"/>
        /// </summary>
        protected override IServiceProvider rootProvider { get { return _appProvider; } }


        public WebShell(IConfiguration config)
        {
            _config = config;
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

            if (UseCors)
            {
                var origins = getConfig("AllowedOrigins").Value ?? DefaultCorsOrigins;
                app.UseRouting();

                app.UseCors(d => d.WithOrigins(origins.Split(","))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
                app.UseEndpoints(e => RegisterEndpointRoutes(e));
            }
            else
            {
                app.UseRouting();
                app.UseEndpoints(e => RegisterEndpointRoutes(e));
            }

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
            var s = context.RequestServices.GetRequiredService<ISpaFallbackHandler>();
            await s.HandleRequestAsync(context);
        }

        public virtual void RegisterEndpointRoutes(IEndpointRouteBuilder routes)
        {

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
            coll.AddTransient<ISpaFallbackHandler, SpaFallbackHandler>();
            coll.AddTransient<IFileUploadService, FileService>();
        }

        protected override IConfigurationSection getConfig(string key)
        {
            return Configuration.GetSection(key);
        }


    }
}
