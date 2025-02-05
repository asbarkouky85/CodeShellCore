using CodeShellCore.Notifications.Devices;

namespace CodeShellCore.Notifications.Pushing
{
    public class NotificationsHub : SignalRHub<INotificationsPushingContract>
    {

        public NotificationsHub()
        {

        }
        public string SetUserConnectionId(long tenantId, object userId)
        {
            return UpdateConnectionData(new SignalRDeviceDataDto
            {
                TenantId = tenantId,
                UserId = long.Parse(userId.ToString())
            });
        }

        public void ClearUserConnectionId(long tenantId, object userId)
        {
            ClearConnectionData(new SignalRDeviceDataDto
            {
                TenantId = tenantId,
                UserId = long.Parse(userId.ToString())
            });
        }
    }
}
