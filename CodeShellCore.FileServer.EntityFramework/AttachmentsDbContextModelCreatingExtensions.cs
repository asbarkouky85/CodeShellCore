using Microsoft.EntityFrameworkCore;

namespace CodeShellCore.FileServer
{
    public static class AttachmentsDbContextModelCreatingExtensions
    {
        public static void ConfigureFileServer(
            this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("Attachments","Atch");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.FullPath).HasMaxLength(300);
                entity.Property(e => e.ContainerName).HasMaxLength(60);

                entity.Property(e => e.Extension).HasMaxLength(10).IsUnicode(false);
                entity.Property(e => e.ContentType).HasMaxLength(500).IsUnicode(false);


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
                entity.ToTable("AttachmentCategories", "Atch");
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FolderPath).HasMaxLength(150);
                entity.Property(e => e.ContainerName).HasMaxLength(60);
                entity.OwnsOne(e => e.MaxDimension, e =>
                {
                    e.Property(d => d.Height).HasColumnName("MaxDimensionHeight");
                    e.Property(d => d.Width).HasColumnName("MaxDimensionWidth");
                });

                entity.Property(e => e.Name)
                  .HasMaxLength(100)
                  .IsUnicode(false);

                entity.Property(e => e.ValidExtensions).HasMaxLength(500).IsUnicode(false);
            });

            modelBuilder.Entity<AttachmentCategoryPermission>(entity =>
            {
                entity.ToTable("AttachmentCategoryPermissions", "Atch");
                entity.Property(e => e.Role).HasMaxLength(100);
                entity.HasOne(d => d.AttachmentCategory)
                .WithMany(d => d.AttachmentCategoryPermissions)
                .HasForeignKey(d => d.AttachmentCategoryId);
            });

            modelBuilder.Entity<AttachmentBinary>(entity =>
            {
                entity.ToTable("AttachmentBinaries", "Atch");
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Bytes).HasMaxLength(4000);
            });

            modelBuilder.Entity<TempFile>(entity =>
            {
                entity.ToTable("TempFiles", "Atch");
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.FileName).HasMaxLength(200);
                entity.Property(e => e.ContentType).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.Extension).HasMaxLength(6).IsUnicode(false);
                entity.Property(e => e.ReferenceId).HasMaxLength(255);
            });

            modelBuilder.Entity<TempFileChunk>(entity =>
            {
                entity.ToTable("TempFileChunks", "Atch");
                entity.HasOne(e => e.TempFile).WithMany(d => d.Chunks).HasForeignKey(e => e.TempFileId);
                entity.Property(e => e.ReferenceId).HasMaxLength(255);
            });

        }
    }
}