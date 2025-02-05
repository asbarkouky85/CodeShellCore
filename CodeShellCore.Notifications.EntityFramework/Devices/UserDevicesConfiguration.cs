using CodeShellCore.Notifications.Users;
using Microsoft.EntityFrameworkCore;

namespace CodeShellCore.Notifications.Devices
{
    public static class UserDevicesConfiguration
    {
        public static void ConfigureCodeShellDevices(this ModelBuilder modelBuilder, string prefix = null, string schema = null)
        {
            modelBuilder.Entity<UserDevice>(entity =>
            {
                entity.ToTable(prefix + "UserDevices", schema);
                
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.DeviceId).IsUnicode().HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(prefix + "Users", schema);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Mobile).IsUnicode(false);

                entity.Property(e => e.PreferredLanguage).IsUnicode(false);
            });

        }
    }
}
