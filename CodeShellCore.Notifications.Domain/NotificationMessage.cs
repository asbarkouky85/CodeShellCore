using CodeShellCore.Helpers;
using CodeShellCore.Notifications.Providers;
using CodeShellCore.Notifications.Senders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Notifications
{
    public class NotificationMessage : AuditedEntity<long>
    {
        private NotificationMessage()
        {
            Id = Utils.GenerateID();
        }
        public NotificationMessage(NotificationProviders provider) : this()
        {
            NotificationProviderId = provider;
            SendingStatusId = NotificationSendingStatus.New;
        }

        public Notification Notification { get; set; }
        public long NotificationId { get; set; }
        public NotificationProvider NotificationProvider { get; set; }
        public NotificationProviders NotificationProviderId { get; set; }
        public NotificationSendingStatus SendingStatusId { get; set; }
        public string RequestContent { get; set; }
        public string Response { get; set; }
        public string Exception { get; set; }

        public void NoDevicesFound()
        {
            SendingStatusId = NotificationSendingStatus.NoDevices;
        }

        public void SetDelivered(MessageDeliveryResult result)
        {
            SendingStatusId = NotificationSendingStatus.Sent;
            RequestContent = result.RequestContent;
            Response = result.ResponseContent;

        }

        public void SetFailed(MessageDeliveryResult result)
        {
            if (NotificationProvider.EnableRetry)
            {
                SendingStatusId = NotificationSendingStatus.Queued;
            }
            else
            {
                SendingStatusId = NotificationSendingStatus.Failed;
            }
            Response = result.ResponseContent;
            RequestContent = result.RequestContent;
            Exception = result.Exception;
        }
    }
}
