using Microsoft.EntityFrameworkCore;

namespace CodeShellCore.Notifications.Types
{
    public static class TypesEfConfiguration
    {
        public static void ConfigureCodeShellNotificationTypes(this ModelBuilder modelBuilder, string prefix = null, string schema = null)
        {
            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.ToTable(prefix + "NotificationTypes", schema);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode().HasMaxLength(255);
                entity.Property(e => e.Code).IsUnicode(false).HasMaxLength(100);

                entity.Navigation(e => e.Templates).AutoInclude();
            });

            modelBuilder.Entity<NotificationTypeTemplate>(entity =>
            {
                entity.ToTable(prefix + "NotificationTypeTemplates", schema);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BodyTemplate).HasMaxLength(5000);
                entity.Property(e => e.TitleTemplate).HasMaxLength(100);
                entity.Property(e => e.Code).HasMaxLength(255);
                entity.Property(e => e.FieldsToUse).HasMaxLength(500);

                

                entity.HasOne(e => e.NotificationType).WithMany(e => e.Templates).HasForeignKey(e => e.NotificationTypeId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.NotificationProvider).WithMany(e => e.Templates).HasForeignKey(e => e.NotificationProviderId)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<NotificationTypeNotificationProvider>(entity =>
            {
                entity.ToTable(prefix + "NotificationTypeNotificationProviders", schema);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(e => e.NotificationType).WithMany(e => e.Providers).HasForeignKey(e => e.NotificationTypeId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.NotificationProvider).WithMany(e => e.NotificationTypes).HasForeignKey(e => e.NotificationProviderId)
                    .OnDelete(DeleteBehavior.Restrict);

            });
        }
    }
}
