using CodeShellCore.Data;
using CodeShellCore.Localization;
using CodeShellCore.Text;
using FMS.Notifications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;

namespace CodeShellCore.Notifications
{
    [EntityName("Notification")]
    public class NotificationListDto : EntityDto<long>, INotificationListDto
    {
        public bool IsRead { get; set; }
        public string Body { get; set; }
        public string Resource { get; set; }
        public long? EntityId { get; set; }
        public string SenderName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ReadOn { get; set; }

    }
}
