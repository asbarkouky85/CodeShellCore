using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Notifications
{
    public enum NotificationSendingStatus
    {
        New = 1,
        Queued = 2,
        Sent = 3,
        Failed = 4,
        NoDevices = 5
    }
}
