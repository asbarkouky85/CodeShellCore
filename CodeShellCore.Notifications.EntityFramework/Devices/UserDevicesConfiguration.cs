using Microsoft.EntityFrameworkCore;

namespace CodeShellCore.Notifications.Devices
{
    public static class UserDevicesConfiguration
    {
        public static void ConfigureUserDevices(this ModelBuilder builder, string prefix = null, string schema = null)
        {
            builder.Entity<UserDevice>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.ToTable(prefix + "UserDevices",schema);
                entity.Property(e => e.DeviceId).IsUnicode().HasMaxLength(255);
            });
        }
    }
}
