using CodeShellCore.Data;
using System.Collections.Generic;

namespace CodeShellCore.Notifications.Senders
{
    public class NotificationMessageDeliveryDto
    {
        public long NotificationId { get; set; }
        public string Body { get; set; }
        public string Parameters { get; set; }
        public string TemplateCode { get; set; }
        public UserMessageDeliveryData User { get; set; }
        public List<UserDeviceData> Devices { get; set; }
        public string Title { get; set; }
        public string EntityType { get; set; }
        public object EntityId { get; set; }
        public string FromDisplayName { get; set; }
        public bool IsHtml { get; set; }

    }
}