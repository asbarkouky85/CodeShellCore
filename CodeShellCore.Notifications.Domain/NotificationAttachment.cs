using CodeShellCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Notifications
{
    public class NotificationAttachment : AuditedEntity<long>
    {
        public Notification Notification { get; set; }
        public long NotificationId { get; set; }
        public string FileName { get; set; }
        public long? AttachmentId { get; set; }
        public string Url { get; set; }
        public NotificationAttachment()
        {
            Id = Utils.GenerateID();
        }
    }
}
