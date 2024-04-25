using CodeShellCore.Text;
using CodeShellCore.Web.Conventions;
using CodeShellCore.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Threading.Tasks;
using CodeShellCore.Types;

namespace CodeShellCore.Web
{
    public abstract class WebShell : Shell
    {
        private string _appRoot;
        private string _sharedPathRoot;
        private IServiceProvider _appProvider;

        public static string AppRootUrl { get { return ((WebShell)App).urlRoot; } }
        protected virtual bool UseHealthChecks => false;
        /// <summary>
        /// (Default : "~")
        /// </summary>
        protected virtual string urlRoot { get { return "~"; } }
        protected override string appRoot { get { return _appRoot; } }
        protected override string sharedPathRoot { get { return _sharedPathRoot; } }
        /// <summary>
        /// public folder (Default : 'wwwroot')
        /// </summary>
        protected override string publicRelativePath => "wwwroot";
        /// <summary>
        /// Adds a fallback middleware that uses <see cref="ISpaFallbackHandler.HandleRequestAsync(HttpContext)"/>
        /// </summary>
        protected virtual bool IsSpa => false;
        protected virtual bool UseCors => false;
        protected virtual bool UseSwagger => false;
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

        public virtual void AddMvcFeatures(IMvcBuilder mvc)
        {

        }

        protected virtual async Task FallbackMiddlewareHandler(HttpContext context, Func<Task> next)
        {
            var s = context.RequestServices.GetRequiredService<ISpaFallbackHandler>();
            await s.HandleRequestAsync(context, next);
        }

        public virtual void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute(
                name: "api",
                template: "api/{controller}/{action}/{id?}"
                );

            routeBuilder.MapRoute(
                    name: "mvc",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" }
                    );
        }

        public override void RegisterServices(IServiceCollection services)
        {
            base.RegisterServices(services);
            services.AddCodeShellApplication();
            services.ConfigureUploads(Configuration);
            var mvc = services.AddControllers();

            services.Configure<MvcOptions>(e =>
            {
                e.Conventions.Add(new CodeShellApplicationModelConvention());
            });

            if (UseSwagger)
            {
                services.AddSwaggerGen();
            }


            if (UseHealthChecks)
            {
                services.AddHealthChecks();
            }

            AddMvcFeatures(mvc);
            mvc.AddNewtonsoftJson(e => e.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ISpaFallbackHandler, SpaFallbackHandler>();
        }

        public virtual void ConfigureHttp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _appRoot = env.ContentRootPath;
            _sharedPathRoot = _config.GetValue<string>("SharedPathRoot");
            _appProvider = app.ApplicationServices;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
            app.UseRouting();

            if (UseSwagger)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    var assembly = Assembly.GetEntryAssembly();
                    var apiName = $"{assembly.GetName().Name}-v{assembly.GetVersionString()}";


                    c.SwaggerEndpoint("/swagger/v1/swagger.json", apiName);
                    c.DocumentTitle = apiName;
                });
            }


            if (UseCors)
            {
                var origins = getConfig("AllowedOrigins").Value ?? DefaultCorsOrigins;
                var originArray = origins.Split(",");
                app.UseCors(d => d.WithOrigins(originArray)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
            }

            app.UseEndpoints(e =>
            {
                RegisterEndpointRoutes(e);
            });

            if (IsSpa)
            {
                app.Use(FallbackMiddlewareHandler);
                app.UseStaticFiles();
            }


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
        }

        public virtual void RegisterEndpointRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllers();
            endpoints.MapControllerRoute(
                name: "api",
                pattern: "api/{controller=Home}/{action=Index}/{id?}"
                );
            endpoints.MapControllerRoute(
                name: "apiArea",
                pattern: "api/{area=app}/{controller=Home}/{action=Index}/{id?}"
                );
            endpoints.MapControllerRoute(
                name: "mvc",
                pattern: "{controller=Home}/{action=Index}/{id?}"
                );
        }

        protected override IConfigurationSection getConfig(string key)
        {
            return Configuration.GetSection(key);
        }


    }
}
