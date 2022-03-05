﻿// <auto-generated />
using System;
using Asga.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Asga.Auth.Migrations
{
    [DbContext(typeof(AuthContext))]
    [Migration("20210612130659_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("Asga.Auth.App", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Name")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Apps", "Auth");
                });

            modelBuilder.Entity("Asga.Auth.Domain", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Domains", "Auth");
                });

            modelBuilder.Entity("Asga.Auth.Resource", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<long>("DomainId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("DomainId");

                    b.ToTable("Resources", "Auth");
                });

            modelBuilder.Entity("Asga.Auth.ResourceAction", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<long>("ResourceId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("ResourceId");

                    b.ToTable("ResourceActions", "Auth");
                });

            modelBuilder.Entity("Asga.Auth.ResourceCollection", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<long>("ResourceId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("ResourceId");

                    b.ToTable("ResourceCollections", "Auth");
                });

            modelBuilder.Entity("Asga.Auth.Role", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Identifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDefaultAppRole")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUserRole")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<long?>("TenantAppId")
                        .HasColumnType("bigint");

                    b.Property<long?>("TenantDomainId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("TenantAppId");

                    b.ToTable("Roles", "Auth");
                });

            modelBuilder.Entity("Asga.Auth.RoleResource", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<bool>("CanDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("CanInsert")
                        .HasColumnType("bit");

                    b.Property<bool>("CanUpdate")
                        .HasColumnType("bit");

                    b.Property<bool>("CanViewDetails")
                        .HasColumnType("bit");

                    b.Property<long?>("CollectionId")
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<long>("ResourceId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.HasIndex("ResourceId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleResources", "Auth");
                });

            modelBuilder.Entity("Asga.Auth.RoleResourceAction", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<long>("ResourceActionId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("ResourceActionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleResourceActions", "Auth");
                });

            modelBuilder.Entity("Asga.Auth.Tenant", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("ntext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Tenants", "Auth");
                });

            modelBuilder.Entity("Asga.Auth.User", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<long?>("AppId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LogonName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Mobile")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Password")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<long?>("PersonId")
                        .HasColumnType("bigint");

                    b.Property<string>("Photo")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<long?>("TenantId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.HasIndex("TenantId");

                    b.ToTable("Users", "Auth");
                });

            modelBuilder.Entity("Asga.Auth.UserEntityLink", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<long>("EntityId")
                        .HasColumnType("bigint");

                    b.Property<string>("EntityName")
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserEntityLinks", "Auth");
                });

            modelBuilder.Entity("Asga.Auth.UserRole", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UpdatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles", "Auth");
                });

            modelBuilder.Entity("Asga.Auth.Resource", b =>
                {
                    b.HasOne("Asga.Auth.Domain", "Domain")
                        .WithMany("Resources")
                        .HasForeignKey("DomainId")
                        .HasConstraintName("FK_Resources_Domains")
                        .IsRequired();

                    b.Navigation("Domain");
                });

            modelBuilder.Entity("Asga.Auth.ResourceAction", b =>
                {
                    b.HasOne("Asga.Auth.Resource", "Resource")
                        .WithMany("ResourceActions")
                        .HasForeignKey("ResourceId")
                        .HasConstraintName("FK_ResourceActions_Resources")
                        .IsRequired();

                    b.Navigation("Resource");
                });

            modelBuilder.Entity("Asga.Auth.ResourceCollection", b =>
                {
                    b.HasOne("Asga.Auth.Resource", "Resource")
                        .WithMany("ResourceCollections")
                        .HasForeignKey("ResourceId")
                        .HasConstraintName("FK_ResourceCollections_Resources")
                        .IsRequired();

                    b.Navigation("Resource");
                });

            modelBuilder.Entity("Asga.Auth.Role", b =>
                {
                    b.HasOne("Asga.Auth.App", "TenantApp")
                        .WithMany("Roles")
                        .HasForeignKey("TenantAppId")
                        .HasConstraintName("FK__Roles__TenantApp__2B3F6F97");

                    b.Navigation("TenantApp");
                });

            modelBuilder.Entity("Asga.Auth.RoleResource", b =>
                {
                    b.HasOne("Asga.Auth.ResourceCollection", "Collection")
                        .WithMany("RoleResources")
                        .HasForeignKey("CollectionId")
                        .HasConstraintName("FK_RoleResources_ResourceCollections");

                    b.HasOne("Asga.Auth.Resource", "Resource")
                        .WithMany("RoleResources")
                        .HasForeignKey("ResourceId")
                        .HasConstraintName("FK_RoleResources_Resources")
                        .IsRequired();

                    b.HasOne("Asga.Auth.Role", "Role")
                        .WithMany("RoleResources")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_RoleResources_Roles")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collection");

                    b.Navigation("Resource");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Asga.Auth.RoleResourceAction", b =>
                {
                    b.HasOne("Asga.Auth.ResourceAction", "ResourceAction")
                        .WithMany("RoleResourceActions")
                        .HasForeignKey("ResourceActionId")
                        .HasConstraintName("FK_RoleResourceActions_ResourceActions")
                        .IsRequired();

                    b.HasOne("Asga.Auth.Role", "Role")
                        .WithMany("RoleResourceActions")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_RoleResourceActions_Roles")
                        .IsRequired();

                    b.Navigation("ResourceAction");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Asga.Auth.User", b =>
                {
                    b.HasOne("Asga.Auth.App", "App")
                        .WithMany("Users")
                        .HasForeignKey("AppId")
                        .HasConstraintName("FK_Users_Apps");

                    b.HasOne("Asga.Auth.Tenant", "Tenant")
                        .WithMany("Users")
                        .HasForeignKey("TenantId")
                        .HasConstraintName("FK_Users_Tenants");

                    b.Navigation("App");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("Asga.Auth.UserEntityLink", b =>
                {
                    b.HasOne("Asga.Auth.User", "User")
                        .WithMany("UserEntityLinks")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserEntityLinks_Users")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Asga.Auth.UserRole", b =>
                {
                    b.HasOne("Asga.Auth.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_UserRoles_Roles")
                        .IsRequired();

                    b.HasOne("Asga.Auth.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserRoles_Users")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Asga.Auth.App", b =>
                {
                    b.Navigation("Roles");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Asga.Auth.Domain", b =>
                {
                    b.Navigation("Resources");
                });

            modelBuilder.Entity("Asga.Auth.Resource", b =>
                {
                    b.Navigation("ResourceActions");

                    b.Navigation("ResourceCollections");

                    b.Navigation("RoleResources");
                });

            modelBuilder.Entity("Asga.Auth.ResourceAction", b =>
                {
                    b.Navigation("RoleResourceActions");
                });

            modelBuilder.Entity("Asga.Auth.ResourceCollection", b =>
                {
                    b.Navigation("RoleResources");
                });

            modelBuilder.Entity("Asga.Auth.Role", b =>
                {
                    b.Navigation("RoleResourceActions");

                    b.Navigation("RoleResources");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Asga.Auth.Tenant", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Asga.Auth.User", b =>
                {
                    b.Navigation("UserEntityLinks");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}