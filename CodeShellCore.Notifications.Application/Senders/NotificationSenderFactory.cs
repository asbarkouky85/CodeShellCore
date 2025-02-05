using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeShellCore.Notifications.Senders
{
    public class NotificationSenderFactory : INotificationSenderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public NotificationSenderFactory(IServiceProvider provider)
        {
            this._serviceProvider = provider;
        }

        public List<INotificationSender> GetSenders(NotificationProviders provider)
        {
            var result = _serviceProvider.GetServices<INotificationSender>();
            return result.Where(e => e.ProviderId == provider).ToList();
        }
    }
}
