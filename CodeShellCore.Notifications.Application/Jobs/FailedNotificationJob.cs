using CodeShellCore.Data.Helpers;
using CodeShellCore.Notifications.Senders;
using CodeShellCore.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications.Jobs
{
    public class FailedNotificationJob : ITimedJob
    {
        public bool RunOnStartUp => true;

        public TimeOfDay? StartOn => new TimeOfDay(1, 0, 0);

        public TimeSpan Interval => new TimeSpan(2, 0, 0);

        public async Task<SubmitResult> Run(IServiceProvider provider)
        {
            await provider.GetService<INotificationDeliveryService>().RetryFailedMessages();
            return new SubmitResult();
        }
    }
}
