using CodeShellCore.Modularity;
using CodeShellCore;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Http.Pushing;
using CodeShellCore.Notifications.Senders;

namespace CodeShellCore.Integration.Firebase
{
    [DependsOn(
        typeof(CodeShellApplicationModule)
        )]
    public class CodeShellIntegrationFirebaseModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddTransient<IFirebaseNotificationService, FirebaseNotificationService>();
            context.Services.AddTransient<INotificationSender, FirebaseFlutterNotificationSender>();
            context.Services.AddTransient<INotificationSender, FirebaseNotificationSender>();
            context.Services.AddOptions<FirebaseOptions>();
            context.Services.Configure<FirebaseOptions>(context.Configuration.GetSection(FirebaseConstants.ConfigKey));

        }
    }
}