using CodeShellCore.Modularity;
using CodeShellCore;
using Microsoft.Extensions.DependencyInjection;
using CodeShellCore.Notifications.Senders;
using CodeShellCore.Integration.Twilio.Http;

namespace CodeShellCore.Integration.Twilio
{
    [DependsOn(
        typeof(CodeShellApplicationModule)
        )]
    public class CodeShellIntegrationTwilioModule : CodeShellModule
    {
        public override void RegisterServices(CodeshellAppContext context)
        {
            context.Services.AddOptions<TwilioOptions>();
            context.Services.Configure<TwilioOptions>(context.Configuration.GetSection(TwilioConstants.ConfigKey));

            context.Services.AddTransient<INotificationSender, TwilioNotificationSender>();
            context.Services.AddTransient<ITwilioHttpService, TwilioHttpService>();
        }
    }
}