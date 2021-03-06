﻿// <auto-generated />
using System;
using CodeShellCore.FileServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeShellCore.FileServer.Migrations
{
    [DbContext(typeof(FileServerDbContext))]
    [Migration("20210208114900_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CodeShellCore.FileServer.Attachment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AttachmentCategoryId");

                    b.Property<long?>("BinaryAttachmentId");

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("FileName")
                        .HasMaxLength(200);

                    b.Property<string>("FullPath")
                        .HasMaxLength(300);

                    b.Property<long?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("AttachmentCategoryId");

                    b.HasIndex("BinaryAttachmentId")
                        .IsUnique()
                        .HasFilter("[BinaryAttachmentId] IS NOT NULL");

                    b.ToTable("Attachments","Atch");
                });

            modelBuilder.Entity("CodeShellCore.FileServer.AttachmentCategory", b =>
                {
                    b.Property<long>("Id");

                    b.Property<long?>("CreatedBy");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("FolderPath")
                        .HasMaxLength(150);

                    b.Property<int>("MaxSize");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<long?>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedOn");

                    b.Property<string>("ValidExtensions")
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("AttachmentCategories","Atch");
                });

            modelBuilder.Entity("CodeShellCore.FileServer.BinaryAttachment", b =>
                {
                    b.Property<long>("Id");

                    b.Property<byte[]>("Bytes");

                    b.HasKey("Id");

                    b.ToTable("BinaryAttachments","Atch");
                });

            modelBuilder.Entity("CodeShellCore.FileServer.Attachment", b =>
                {
                    b.HasOne("CodeShellCore.FileServer.AttachmentCategory", "AttachmentCategory")
                        .WithMany("Attachments")
                        .HasForeignKey("AttachmentCategoryId")
                        .HasConstraintName("FK_Attachments_AttachmentCategories");

                    b.HasOne("CodeShellCore.FileServer.BinaryAttachment", "BinaryAttachment")
                        .WithOne("Attachment")
                        .HasForeignKey("CodeShellCore.FileServer.Attachment", "BinaryAttachmentId")
                        .HasConstraintName("FK_Attachments_BinaryAttachments")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
