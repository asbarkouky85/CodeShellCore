using CodeShellCore.Data;
using CodeShellCore.Notifications.Pushing;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications.Senders
{
    public class ListNotificationSender : DomainService<INotificationsUnit>, INotificationSender, IListNotificationSender
    {
        IEmitter<INotificationsPushingContract> pusher => Store.GetService<IEmitter<INotificationsPushingContract>>();

        public NotificationProviders ProviderId => NotificationProviders.List;

        public ListNotificationSender(INotificationsUnit unit) : base(unit)
        {
        }

        public async Task<MessageDeliveryResult> SendAsync(NotificationMessageDeliveryDto deliveryData)
        {
            if (deliveryData.User.UserId != null)
            {
                var userNotificationCount = await Unit.UserRepository.FindSingleAs(d => new
                {
                    d.Id,
                    Count = d.Notifications.Count(e => !e.IsRead && e.NotificationMessages.Any(e => e.NotificationProviderId == NotificationProviders.List))
                }, d => d.Id == deliveryData.User.UserId);
                var count = userNotificationCount.Count;
                await pusher.EmitAsync(d => d.NotificationsChanged(count), deliveryData.Devices.Select(e => e.ConnectionId).ToArray());
            }
            return new MessageDeliveryResult(true);
        }

        public async Task<MessageDeliveryResult> SendCount(NotificationCountSendDto deliveryData)
        {
            var res = new MessageDeliveryResult();
            foreach (var user in deliveryData.UserIds)
            {
                var devices = await Unit.UserDeviceRepository.GetDevices(new Devices.DevicesRequest
                {
                    UserId = user,
                    DeviceType = NotificationProviders.Browser
                });

                if (devices.Any())
                {
                    var request = new NotificationMessageDeliveryDto
                    {
                        User = new UserMessageDeliveryData { UserId = user },
                        Devices = devices.Select(e => new UserDeviceData
                        {
                            DeviceId = e.DeviceId,
                            ConnectionId = e.ConnectionId,
                        }).ToList()
                    };
                    await SendAsync(request);
                }

            }
            return res;
        }
    }
}
