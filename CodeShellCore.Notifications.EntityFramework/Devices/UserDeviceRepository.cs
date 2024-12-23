using CodeShellCore.Data.EntityFramework;
using CodeShellCore.Notifications.Devices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codeshell.Abp.EntityFrameworkCore.Devices
{
    public class UserDeviceRepository<TContext> : KeyRepository<UserDevice, TContext, long>, IUserDeviceRepository
        where TContext : DbContext, IDevicesDbContext
    {
        public UserDeviceRepository(TContext con) : base(con)
        {
        }

        public virtual async Task<UserDevice> GetByDeviceId(string deviceId)
        {
            var q = Loader;
            UserDevice userDevice = await q.FirstOrDefaultAsync(e => e.DeviceId == deviceId);
            return userDevice;
        }

        public virtual async Task<List<UserDevice>> GetDevices(DevicesRequest req)
        {
            var q = Loader;
            if (req.DeviceType != null)
                q = q.Where(w => w.DeviceTypeId == req.DeviceType && (w.IsLoggedIn || req.DeviceType != DeviceTypes.Mobile));
            if (req.UserId != null)
                q = q.Where(e => e.UserId == req.UserId);
            if (req.UserIds != null)
                q = q.Where(e => req.UserIds.Contains(e.UserId));

            return await q.ToListAsync();
        }
    }
}
