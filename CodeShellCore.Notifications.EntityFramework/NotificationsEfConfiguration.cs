using CodeShellCore.Notifications.Devices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications
{
    public static class NotificationsEfConfiguration
    {
        public static void ConfigureCodeShellNotifications(this ModelBuilder modelBuilder, string prefix = null, string schema = null)
        {
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable(prefix + "Notifications", schema);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EntityType).IsUnicode(false);

                entity.HasOne(e => e.NotificationType).WithMany().HasForeignKey(e => e.NotificationTypeId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notifications_Users");
            });

            modelBuilder.Entity<NotificationAttachment>(entity =>
            {
                entity.ToTable(prefix + "NotificationAttachments", schema);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Url).HasMaxLength(400);
                entity.Property(e => e.FileName).HasMaxLength(255);

                entity.HasOne(e => e.Notification).WithMany(e => e.NotificationAttachments).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<NotificationMessage>(entity =>
            {
                entity.ToTable(prefix + "NotificationMessages", schema);
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.RequestContent).HasMaxLength(400);
                entity.Property(e => e.Response).HasMaxLength(1024);

                entity.HasOne(e => e.Notification).WithMany(e => e.NotificationMessages).HasForeignKey(e=>e.NotificationId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.NotificationProvider).WithMany().HasForeignKey(e => e.NotificationProviderId).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
