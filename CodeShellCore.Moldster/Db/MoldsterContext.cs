﻿using System;using Microsoft.EntityFrameworkCore;using Microsoft.EntityFrameworkCore.Metadata;namespace CodeShellCore.Moldster.Db{    public partial class MoldsterContext : DbContext    {        public MoldsterContext()        {        }        public MoldsterContext(DbContextOptions<MoldsterContext> options)            : base(options)        {        }        public virtual DbSet<Client> Clients { get; set; }        public virtual DbSet<Control> Controls { get; set; }        public virtual DbSet<ControlValidator> ControlValidators { get; set; }        public virtual DbSet<Domain> Domains { get; set; }        public virtual DbSet<DomainEntity> DomainEntities { get; set; }        public virtual DbSet<DomainEntityCollection> DomainEntityCollections { get; set; }        public virtual DbSet<DomainEntityProperty> DomainEntityProperties { get; set; }        public virtual DbSet<EntityCollectionCondition> EntityCollectionConditions { get; set; }        public virtual DbSet<Page> Pages { get; set; }        public virtual DbSet<PageCategory> PageCategories { get; set; }        public virtual DbSet<PageControl> PageControls { get; set; }        public virtual DbSet<PageControlValidator> PageControlValidators { get; set; }        public virtual DbSet<Resource> Resources { get; set; }        public virtual DbSet<ResourceAction> ResourceActions { get; set; }        public virtual DbSet<Tenant> Tenants { get; set; }        public virtual DbSet<TenantApp> TenantApps { get; set; }        public virtual DbSet<User> Users { get; set; }        public virtual DbSet<Validator> Validators { get; set; }        protected override void OnModelCreating(ModelBuilder modelBuilder)        {            modelBuilder.Entity<Client>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Address).IsUnicode(false);                entity.Property(e => e.Identifier).IsUnicode(false);                entity.Property(e => e.Secret).IsUnicode(false);            });            modelBuilder.Entity<Control>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.ControlType).IsUnicode(false);                entity.Property(e => e.Identifier).IsUnicode(false);                entity.HasOne(d => d.DomainEntityProperty)                    .WithMany(p => p.Controls)                    .HasForeignKey(d => d.DomainEntityPropertyId)                    .HasConstraintName("FK_Controls_DomainEntityProperties");                entity.HasOne(d => d.PageCategory)                    .WithMany(p => p.Controls)                    .HasForeignKey(d => d.PageCategoryId)                    .OnDelete(DeleteBehavior.Cascade)                    .HasConstraintName("FK_Controls_PageCategories");                entity.HasOne(d => d.ParentControlNavigation)                    .WithMany(p => p.InverseParentControlNavigation)                    .HasForeignKey(d => d.ParentControl)                    .HasConstraintName("FK_PageControls_PageControls");            });            modelBuilder.Entity<ControlValidator>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.HasOne(d => d.Control)                    .WithMany(p => p.ControlValidators)                    .HasForeignKey(d => d.ControlId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_ControlValidators_Controls");                entity.HasOne(d => d.Validator)                    .WithMany(p => p.ControlValidators)                    .HasForeignKey(d => d.ValidatorId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_ControlValidators_Validators");            });            modelBuilder.Entity<Domain>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Chain).IsUnicode(false);                entity.Property(e => e.Name).IsUnicode(false);            });            modelBuilder.Entity<DomainEntity>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Name).IsUnicode(false);                entity.HasOne(d => d.Domain)                    .WithMany(p => p.DomainEntities)                    .HasForeignKey(d => d.DomainId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_DomainEntities_Domains");            });            modelBuilder.Entity<DomainEntityCollection>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Name).IsUnicode(false);                entity.HasOne(d => d.DomainEntity)                    .WithMany(p => p.DomainEntityCollections)                    .HasForeignKey(d => d.DomainEntityId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_DomainEntityCollections_DomainEntities");            });            modelBuilder.Entity<DomainEntityProperty>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.DataType).IsUnicode(false);                entity.HasOne(d => d.DomainEntity)                    .WithMany(p => p.DomainEntityPropertyDomainEntities)                    .HasForeignKey(d => d.DomainEntityId)                    .HasConstraintName("FK_DomainEntityProperties_DomainEntities");                entity.HasOne(d => d.ReferenceEntity)                    .WithMany(p => p.DomainEntityPropertyReferenceEntities)                    .HasForeignKey(d => d.ReferenceEntityId)                    .HasConstraintName("FK_DomainEntityProperties_DomainEntities1");            });            modelBuilder.Entity<EntityCollectionCondition>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Property).IsUnicode(false);                entity.HasOne(d => d.DomainEntityCollection)                    .WithMany(p => p.EntityCollectionConditions)                    .HasForeignKey(d => d.DomainEntityCollectionId)                    .HasConstraintName("FK_EntityCollectionConditions_DomainEntityCollections");            });            modelBuilder.Entity<Page>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Apps).IsUnicode(false);                entity.Property(e => e.Layout).IsUnicode(false);                entity.Property(e => e.Name).IsUnicode(false);                entity.Property(e => e.PrivilegeType).IsUnicode(false);                entity.Property(e => e.RouteParameters).IsUnicode(false);                entity.Property(e => e.SpecialPermission).IsUnicode(false);                entity.Property(e => e.ViewPath).IsUnicode(false);                entity.HasOne(d => d.Domain)                    .WithMany(p => p.Pages)                    .HasForeignKey(d => d.DomainId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_Pages_Domains");                entity.HasOne(d => d.PageCategory)                    .WithMany(p => p.Pages)                    .HasForeignKey(d => d.PageCategoryId)                    .HasConstraintName("FK_Pages_PageCategories");                entity.HasOne(d => d.ResourceAction)                    .WithMany(p => p.Pages)                    .HasForeignKey(d => d.ResourceActionId)                    .HasConstraintName("FK_Pages_ResourceActions");                entity.HasOne(d => d.Resource)                    .WithMany(p => p.Pages)                    .HasForeignKey(d => d.ResourceId)                    .HasConstraintName("FK_Pages_Resources");                entity.HasOne(d => d.SourceCollection)                    .WithMany(p => p.Pages)                    .HasForeignKey(d => d.SourceCollectionId)                    .HasConstraintName("FK_Pages_DomainEntityCollections1");                entity.HasOne(d => d.Tenant)                    .WithMany(p => p.Pages)                    .HasForeignKey(d => d.TenantId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_Pages_Tenants");            });            modelBuilder.Entity<PageCategory>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.BaseComponent).IsUnicode(false);                entity.Property(e => e.Layout).IsUnicode(false);                entity.Property(e => e.Name).IsUnicode(false);                entity.Property(e => e.ScriptPath).IsUnicode(false);                entity.Property(e => e.ViewPath).IsUnicode(false);                entity.HasOne(d => d.DomainEntity)                    .WithMany(p => p.PageCategories)                    .HasForeignKey(d => d.DomainEntityId)                    .HasConstraintName("FK_PageCategories_DomainEntities1");                entity.HasOne(d => d.Domain)                    .WithMany(p => p.PageCategories)                    .HasForeignKey(d => d.DomainId)                    .HasConstraintName("FK_PageCategories_Domains");                entity.HasOne(d => d.IdNavigation)                    .WithOne(p => p.InverseIdNavigation)                    .HasForeignKey<PageCategory>(d => d.Id)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_PageCategories_PageCategories");                entity.HasOne(d => d.Resource)                    .WithMany(p => p.PageCategories)                    .HasForeignKey(d => d.ResourceId)                    .HasConstraintName("FK_PageCategories_Resources");            });            modelBuilder.Entity<PageControl>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.HasOne(d => d.Control)                    .WithMany(p => p.PageControls)                    .HasForeignKey(d => d.ControlId)                    .HasConstraintName("FK_PageControls_Controls");                entity.HasOne(d => d.Page)                    .WithMany(p => p.PageControls)                    .HasForeignKey(d => d.PageId)                    .HasConstraintName("FK_PageControls_Pages");                entity.HasOne(d => d.SourceCollection)                    .WithMany(p => p.PageControls)                    .HasForeignKey(d => d.SourceCollectionId)                    .HasConstraintName("FK_PageControls_DomainEntityCollections");            });            modelBuilder.Entity<PageControlValidator>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.HasOne(d => d.PageControl)                    .WithMany(p => p.PageControlValidators)                    .HasForeignKey(d => d.PageControlId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_PageControlValidators_PageControls");                entity.HasOne(d => d.Validator)                    .WithMany(p => p.PageControlValidators)                    .HasForeignKey(d => d.ValidatorId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_PageControlValidators_Validators");            });            modelBuilder.Entity<Resource>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Name).IsUnicode(false);                entity.Property(e => e.ServiceName).IsUnicode(false);                entity.HasOne(d => d.Domain)                    .WithMany(p => p.Resources)                    .HasForeignKey(d => d.DomainId)                    .HasConstraintName("FK_Resources_Domains");            });            modelBuilder.Entity<ResourceAction>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Name).IsUnicode(false);                entity.HasOne(d => d.Resource)                    .WithMany(p => p.ResourceActions)                    .HasForeignKey(d => d.ResourceId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_ResourceActions_Resources");                entity.HasOne(d => d.Tenant)                    .WithMany(p => p.ResourceActions)                    .HasForeignKey(d => d.TenantId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_ResourceActions_Tenants");            });            modelBuilder.Entity<Tenant>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Code).IsUnicode(false);            });            modelBuilder.Entity<TenantApp>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Name).IsUnicode(false);                entity.HasOne(d => d.Tenant)                    .WithMany(p => p.TenantApps)                    .HasForeignKey(d => d.TenantId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_TenantApps_Tenants");            });            modelBuilder.Entity<User>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.LogonName).IsUnicode(false);            });            modelBuilder.Entity<Validator>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.CalendarType).IsUnicode(false);                entity.Property(e => e.MaxValue).IsUnicode(false);                entity.Property(e => e.MinValue).IsUnicode(false);                entity.Property(e => e.Pattern).IsUnicode(false);                entity.Property(e => e.Type).IsUnicode(false);            });        }    }}