using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications.Devices
{
    public interface IUserDeviceRepository : IKeyRepository<UserDevice, long>
    {
        Task<List<UserDevice>> GetDevices(DevicesRequest req);
        Task<UserDevice> GetByDeviceId(string deviceId);

    }
}
