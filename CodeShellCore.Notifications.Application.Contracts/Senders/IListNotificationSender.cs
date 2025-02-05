using System.Threading.Tasks;

namespace CodeShellCore.Notifications.Senders
{
    public interface IListNotificationSender
    {
        Task<MessageDeliveryResult> SendAsync(NotificationMessageDeliveryDto deliveryData);
        Task<MessageDeliveryResult> SendCount(NotificationCountSendDto deliveryData);
    }
}