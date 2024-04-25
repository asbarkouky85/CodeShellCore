using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Notifications.Devices
{
    public class SignalRDeviceDataDto
    {
        public long UserId { get; set; }
        public string DeviceId { get; set; }
        public string Culture { get; set; }
        public long? TenantId { get; set; }
        public string ConnectionId { get; set; }
    }
}
