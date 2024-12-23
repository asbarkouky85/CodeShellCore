using CodeShellCore.Files.Logging;
using CodeShellCore.Modularity;
using CodeShellCore.Proxy;
using CodeShellCore.Text;
using CodeShellCore.Types;
using CodeShellCore.Web.AuditLogs;
using CodeShellCore.Web.Conventions;
using CodeShellCore.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace CodeShellCore.Web
{
    [DependsOn(
        typeof(CodeShellApplicationContractsModule)
        )]
    public class CodeShellWebModule : CodeShellModule
    {

        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddSingleton<AuditLoggingMiddleware>();
            var mvc = context.Services.AddControllers();

            context.Services.Configure<MvcOptions>(e =>
            {
                e.Conventions.Add(new CodeShellApplicationModelConvention());
            });

            context.Services.AddSwaggerGen();
            context.Services.AddHealthChecks();

            //mvc.AddJsonOptions(e => e.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local);

            context.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public override void Configure(CodeShellApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();
            var webOptions = context.ServiceProvider.GetRequiredService<IOptions<CodeShellWebAppOptions>>();

            if (webOptions.Value.UseAuditLogs)
                app.UseMiddleware<AuditLoggingMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = async cont =>
                {

                    var feat = cont.Features.Get<IExceptionHandlerPathFeature>();
                    await cont.HandleErrorAsync(feat.Error);

                }
            });
            app.UseRouting();

            if (webOptions.Value.UseSwagger)
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


            if (webOptions.Value.UseCors)
            {
                var origins = Shell.GetConfig("AllowedOrigins").Value ?? webOptions.Value.DefaultCorsOrigins;
                var originArray = origins.Split(",");

                app.UseCors(d => d.WithOrigins(originArray)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("Content-Disposition")
                    .AllowCredentials());
            }

            app.UseEndpoints(e =>
            {
                RegisterEndpointRoutes(e);
            });

            app.UseStaticFiles();


            if (webOptions.Value.UseHealthCheck)
            {
                app.UseHealthChecks("/health", new HealthCheckOptions()
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

        private void RegisterEndpointRoutes(IEndpointRouteBuilder endpoints)
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

    }
}
