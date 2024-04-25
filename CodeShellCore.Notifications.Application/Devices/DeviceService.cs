using CodeShellCore.Data.Services;
using CodeShellCore.Services;
using System;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications.Devices
{
    public class DeviceService : DataService<ICodeShellNotificationsUnit>, IDeviceService
    {
        public DeviceService(ICodeShellNotificationsUnit unit) : base(unit)
        {
        }

        public async Task ClearSingalRConnectionData(SignalRDeviceDataDto data)
        {
            var dev = await Unit.UserDeviceRepository.GetByDeviceId(data.DeviceId);
            if (dev != null)
            {
                await Unit.UserDeviceRepository.DeleteAsync(dev);
            }
            await Unit.SaveChangesAsync();
        }

        public async Task UpdateSignalRConnectionData(SignalRDeviceDataDto data)
        {
            var dev = await Unit.UserDeviceRepository.GetByDeviceId(data.DeviceId);
            if (dev == null)
            {
                dev = new UserDevice(data.DeviceId, data.UserId, data.TenantId, DeviceTypes.Browser);
                await Unit.UserDeviceRepository.InsertAsync(dev);
            }
            dev.SetConnectionId(data.ConnectionId);
            await Unit.SaveChangesAsync();
        }
    }
}
