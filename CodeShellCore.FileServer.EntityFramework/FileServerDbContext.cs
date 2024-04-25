using CodeShellCore.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.FileServer
{
    public class FileServerDbContext : CodeShellDbContext<FileServerDbContext>
    {
        protected override string ConnectionStringKey => "FileServer";
        public FileServerDbContext(DbContextOptions<FileServerDbContext> opts) : base(opts)
        {
            
        }

        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<AttachmentCategory> AttachmentCategories { get; set; }
        public virtual DbSet<AttachmentBinary> BinaryAttachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureFileServer();
        }
    }
}
