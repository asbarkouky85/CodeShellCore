using CodeShellCore.Data.Helpers;
using CodeShellCore.Notifications.Senders;
using CodeShellCore.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications.Jobs
{
    public class NotificationSenderJob : ITimedJob
    {
        public bool RunOnStartUp => true;

        public TimeOfDay? StartOn => null;

        public TimeSpan Interval => new TimeSpan(1, 0, 0);

        public async Task<SubmitResult> Run(IServiceProvider provider)
        {
            var service = provider.GetRequiredService<INotificationDeliveryService>();
            await service.SendPendingMessages();
            return new SubmitResult(0);
        }
    }
}
