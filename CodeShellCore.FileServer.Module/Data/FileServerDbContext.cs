using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.FileServer.Data
{
    public class FileServerDbContext : DbContext
    {
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<AttachmentCategory> AttachmentCategories { get; set; }
        public virtual DbSet<BinaryAttachment> BinaryAttachments { get; set; }

        public FileServerDbContext(DbContextOptions<FileServerDbContext> opts) : base(opts)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("Attachments");

                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FullPath).HasMaxLength(300);

                entity.HasOne(d => d.AttachmentCategory)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.AttachmentCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attachments_AttachmentCategories");

                entity.HasOne(d => d.BinaryAttachment)
                    .WithOne(p => p.Attachment)
                    .HasForeignKey<Attachment>(d => d.BinaryAttachmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Attachments_BinaryAttachments");

            });

            modelBuilder.Entity<AttachmentCategory>(entity =>
            {
                entity.ToTable("AttachmentCategories");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.FolderPath).HasMaxLength(150);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValidExtensions).IsUnicode(false);
            });

            modelBuilder.Entity<BinaryAttachment>(entity =>
            {
                entity.ToTable("BinaryAttachments");
                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }
    }
}
