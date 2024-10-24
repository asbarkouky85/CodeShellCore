using CodeShellCore.Http.Pushing;
using CodeShellCore.Modularity;
using CodeShellCore.Moldster;
using CodeShellCore.Moldster.Builder;
using CodeShellCore.Moldster.Tracing;
using CodeShellCore.Notifications;
using CodeShellCore.Security.Sessions;
using CodeShellCore.Web.Razor.Elements;
using CodeShellCore.Web.Razor.Elements.Angular;
using CodeShellCore.Web.Razor.General;
using CodeShellCore.Web.Razor.Services;
using CodeShellCore.Web.Razor.SignalR;
using CodeShellCore.Web.Razor.Tables;
using CodeShellCore.Web.Razor.Tables.Angular;
using CodeShellCore.Web.Razor.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Web.Razor.Moldster;
using CodeShellCore.Cli;

namespace CodeShellCore.Web.Razor
{
    [DependsOn(
        typeof(CodeShellWebModule),
        typeof(MoldsterApplicationContractsModule))]
    public class MoldsterWebModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            var legacy = context.Configuration.GetSection("Moldster:Legacy").Get<bool>();
            if (legacy)
            {
                context.Services.AddTransient<IViewsService, LegacyRazorViewsService>();
            }
            else
            {
                context.Services.AddTransient<IViewsService, RazorViewsService>();
            }

            context.Services.AddTransient<IMoldsterRazorRenderingService, MoldsterRazorRenderingService>();

            context.Services.AddTransient<IPushingSessionManager, ConfigSessionManager>();
            context.Services.AddTransient<ISessionManager, ConfigSessionManager>();
            context.Services.AddTransient<ILayoutsService, RazorPathsProvider>();

            context.Services.AddSignalR();
            context.Services.AddSignalRHub<IOutputMessageSender, GenerationHub>();
            context.Services.AddSignalRHub<IBundlingTasksNotifications, TasksHub>();

            context.Services.AddSingleton<IRazorLocaleTextProvider, AngularTextProvider>();
            context.Services.AddScoped<IElementsHelper, AngularElementsHelper>();
            context.Services.AddScoped<IAngularElementsHelper, AngularElementsHelper>();
            context.Services.AddScoped<ITablesHelper, AngularTablesHelper>();
            context.Services.AddScoped<IAngularTablesHelper, AngularTablesHelper>();
            context.Services.AddScoped<IGeneralHelper, DefaultGeneralHelper>();
            context.Services.AddScoped<IOutputWriter, MessagePusherOutputService>();
            context.Services.Configure<CodeShellAppOptions>(opt =>
            {
                opt.DefaultCulture = "en-US";
                opt.UseLocalization = false;
            });

            context.Services.Configure<CodeShellWebAppOptions>(e =>
            {
                e.UseSwagger = true;
                e.UseHealthCheck = true;
                e.UseCors = true;
                e.DefaultCorsOrigins = "http://localhost:8050,http://localhost:4200,http://127.0.0.1:8050,http://127.0.0.1:8051";
            });

            context.Services.AddMoldsterRazorHelpers();

            context.Services.AddRazorPages().AddRazorRuntimeCompilation();


        }

        public override void Configure(CodeShellApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseEndpoints(e =>
            {
                e.MapHub<GenerationHub>("/generationHub");
                e.MapHub<TasksHub>("/tasksHub");
                e.MapControllerRoute(
                name: "apiArea",
                pattern: "apiAction/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }

        public override void OnApplicationStarted(CodeShellApplicationInitializationContext context)
        {
            RazorConfig.UseAngular2Razor();
            SearchExpressions.RegisterExpressions();
        }
    }
}

