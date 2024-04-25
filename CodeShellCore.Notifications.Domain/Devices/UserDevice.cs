using CodeShellCore.Helpers;
using CodeShellCore.MultiTenant;
using System;

namespace CodeShellCore.Notifications.Devices
{
    public class UserDevice : AuditedEntity<long>, IMultiTenant
    {

        protected UserDevice() : base()
        {
            Id = Utils.GenerateID();
        }

        public UserDevice(string deviceId, long userId, long? tenantId, DeviceTypes deviceType, bool isLoggedIn = true) : this()
        {
            DeviceId = deviceId;
            UserId = userId;
            TenantId = tenantId;
            DeviceTypeId = deviceType;
            IsLoggedIn = isLoggedIn;
        }

        public string DeviceId { get; set; }
        public string ConnectionId { get; set; }
        public DeviceTypes DeviceTypeId { get; set; }
        public long? TenantId { get; set; }
        public long UserId { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool? IsFlutter { get; set; }

        public void SetConnectionId(string connection)
        {
            ConnectionId = connection;
        }
    }
}
