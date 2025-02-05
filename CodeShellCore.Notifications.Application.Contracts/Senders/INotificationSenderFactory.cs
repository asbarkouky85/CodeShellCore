using System.Collections.Generic;

namespace CodeShellCore.Notifications.Senders
{
    public interface INotificationSenderFactory
    {
        List<INotificationSender> GetSenders(NotificationProviders provider);
    }
}