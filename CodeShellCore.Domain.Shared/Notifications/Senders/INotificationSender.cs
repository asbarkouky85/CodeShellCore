using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications.Senders
{
    public interface INotificationSender
    {
        NotificationProviders ProviderId { get; }
        Task<MessageDeliveryResult> SendAsync(NotificationMessageDeliveryDto deliveryData);
    }
}
