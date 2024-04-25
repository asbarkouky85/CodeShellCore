using Microsoft.EntityFrameworkCore;

namespace CodeShellCore.Notifications.Devices
{
    public interface IDevicesDbContext
    {
        DbSet<UserDevice> UserDevices { get; set; }
    }
}
