using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeShellCore.Notifications.Devices
{
    public class DevicesRequest
    {
        public IEnumerable<long> UserIds { get; set; }
        public long? UserId { get; set; }
        public DeviceTypes? DeviceType { get; set; }
    }
}