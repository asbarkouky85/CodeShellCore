using CodeShellCore.Notifications.Types;
using Microsoft.EntityFrameworkCore;

namespace CodeShellCore.Notifications.Providers
{
    public static class ProvidersEfConfiguration
    {
        public static void ConfigureCodeShellNotificationProviders(this ModelBuilder modelBuilder, string prefix = null, string schema = null)
        {
            modelBuilder.Entity<NotificationProvider>(entity =>
            {
                entity.ToTable(prefix + "NotificationProvider", schema);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode().HasMaxLength(255);

            });

        }
    }
}
