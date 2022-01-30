﻿using System;using Microsoft.EntityFrameworkCore;using Microsoft.EntityFrameworkCore.Metadata;namespace Asga.Mobile{    public partial class AsgaMobileContext : DbContext    {        public AsgaMobileContext()        {        }        public AsgaMobileContext(DbContextOptions<AsgaMobileContext> options)            : base(options)        {        }        public virtual DbSet<Notification> Notifications { get; set; }        public virtual DbSet<UserDevice> UserDevices { get; set; }        public virtual DbSet<UserNotification> UserNotifications { get; set; }        public virtual DbSet<UserReminder> UserReminders { get; set; }        protected override void OnModelCreating(ModelBuilder modelBuilder)        {            modelBuilder.Entity<Notification>(entity =>            {                entity.Property(e => e.EntityType).IsUnicode(false);                entity.Property(e => e.TextId).IsUnicode(false);            });            modelBuilder.Entity<UserNotification>(entity =>            {                entity.HasOne(d => d.Notification)                    .WithMany(p => p.UserNotifications)                    .HasForeignKey(d => d.NotificationId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_UserNotifications_Notifications");            });            modelBuilder.Entity<UserReminder>(entity =>            {                entity.Property(e => e.EntityType).IsUnicode(false);                entity.Property(e => e.TextId).IsUnicode(false);            });        }    }}