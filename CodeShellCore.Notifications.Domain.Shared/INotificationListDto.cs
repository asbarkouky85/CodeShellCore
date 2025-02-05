using System;
using System.Collections.Generic;

namespace FMS.Notifications
{
    public interface INotificationListDto
    {
        string Body { get; set; }
        DateTime? CreatedOn { get; set; }
        long? EntityId { get; set; }
        bool IsRead { get; set; }
        DateTime? ReadOn { get; set; }
        string Resource { get; set; }
        string SenderName { get; set; }
    }
}