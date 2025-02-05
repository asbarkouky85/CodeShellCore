using CodeShellCore.Http.Pushing;
using CodeShellCore.Integration.Firebase.Flutter;
using CodeShellCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications.Senders
{
    public class FirebaseFlutterNotificationSender : ApplicationService, INotificationSender
    {
        IFirebaseNotificationService Service => Store.GetService<IFirebaseNotificationService>();

        public NotificationProviders ProviderId => NotificationProviders.FireBaseFlutter;

        public FirebaseFlutterNotificationSender(IServiceProvider provider) : base(provider)
        {
        }

        public async Task<MessageDeliveryResult> SendAsync(NotificationMessageDeliveryDto deliveryData)
        {
            var result = new MessageDeliveryResult();
            foreach (var device in deliveryData.Devices)
            {
                var deviceResult = new DeviceMessageDeliveryResult();
                deviceResult.DeviceId = device.DeviceId;
                try
                {
                    var msgBody = new FirebaseFlutterRequest
                    {
                        registration_ids = new List<string> { device.ConnectionId },
                        content_available = true,
                        apnspriority = 5,
                        data = new
                        {
                            body = deliveryData.Body,
                            title = deliveryData.Title,
                            entityType = deliveryData.EntityType,
                            entityId = deliveryData.EntityId,
                            notificationId = deliveryData.NotificationId
                        }
                    };
                    deviceResult.RequestContent = msgBody.ToString();
                    var sendResult = await Service.SendNotificationToFlutter(msgBody);
                    deviceResult.ResponseContent = sendResult.ToString();
                    deviceResult.IsSuccess = sendResult.IsSuccess;

                }
                catch (Exception ex)
                {
                    deviceResult.Exception = ex.GetMessageAndStack();
                    deviceResult.SetException(ex);
                }
                result.DeviceResults.Add(deviceResult);

            }
            result.IsSuccess = result.DeviceResults.Any(e => e.IsSuccess);
            return result;
        }
    }
}
