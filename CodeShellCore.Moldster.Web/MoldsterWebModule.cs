using CodeShellCore.Cli;
using CodeShellCore.Modularity;
using CodeShellCore.Moldster.Builder;
using CodeShellCore.Moldster.Tracing;
using CodeShellCore.Notifications;
using CodeShellCore.Web;
using CodeShellCore.Web.Razor.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CodeShellCore.Moldster
{
    [DependsOn(
        typeof(MoldsterApplicationContractsModule),
        typeof(CodeShellNotificationsWebModule),
        typeof(CodeShellWebModule)
        )]
    public class MoldsterWebModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddScoped<IOutputWriter, MessagePusherOutputService>();

            context.Services.AddSignalR();
            context.Services.AddSignalRHub<IOutputMessageSender, GenerationHub>();
            context.Services.AddSignalRHub<IBundlingTasksNotifications, TasksHub>();

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
    }
}