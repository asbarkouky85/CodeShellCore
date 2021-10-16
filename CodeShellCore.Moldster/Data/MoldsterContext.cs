﻿using System;using System.Data.SqlClient;
using CodeShellCore.Moldster.Dto;
using CodeShellCore.Text;
using Microsoft.EntityFrameworkCore;using Microsoft.EntityFrameworkCore.Metadata;namespace CodeShellCore.Moldster{    public partial class MoldsterContext : DbContext    {        public MoldsterContext(DbContextOptions<MoldsterContext> options)            : base(options)        {        }        [DbFunction]        public static string GetPath(string entity, string chain)        {            throw new NotImplementedException();        }        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)        {            if (!optionsBuilder.IsConfigured)                optionsBuilder.UseSqlServer(Shell.GetConfigAs<string>("ConnectionStrings:Moldster"));        }        public SyncResult SyncTenants(long source, long target)        {            SqlParameter par = new SqlParameter("res", System.Data.SqlDbType.VarChar, -1) { Direction = System.Data.ParameterDirection.Output };            Database.ExecuteSqlRaw("exec dbo.SyncTenants @p0,@p1,@res OUTPUT", source, target, par);            if (par.Value != null)                return par.Value.ToString().FromJson<SyncResult>();            return null;        }        public virtual DbSet<App> Apps { get; set; }        public virtual DbSet<Client> Clients { get; set; }        public virtual DbSet<Control> Controls { get; set; }        public virtual DbSet<ControlValidator> ControlValidators { get; set; }        public virtual DbSet<CustomField> CustomFields { get; set; }        public virtual DbSet<CustomText> CustomTexts { get; set; }        public virtual DbSet<Domain> Domains { get; set; }        public virtual DbSet<NavigationGroup> NavigationGroups { get; set; }        public virtual DbSet<NavigationPage> NavigationPages { get; set; }        public virtual DbSet<Page> Pages { get; set; }        public virtual DbSet<PageCategory> PageCategories { get; set; }        public virtual DbSet<PageCategoryParameter> PageCategoryParameters { get; set; }        public virtual DbSet<PageControl> PageControls { get; set; }        public virtual DbSet<PageControlValidator> PageControlValidators { get; set; }        public virtual DbSet<PageParameter> PageParameters { get; set; }        public virtual DbSet<PageRoute> PageRoutes { get; set; }        public virtual DbSet<Resource> Resources { get; set; }        public virtual DbSet<ResourceAction> ResourceActions { get; set; }        public virtual DbSet<ResourceCollection> ResourceCollections { get; set; }        public virtual DbSet<ResourceCollectionCondition> ResourceCollectionConditions { get; set; }        public virtual DbSet<Tenant> Tenants { get; set; }        public virtual DbSet<Validator> Validators { get; set; }        protected override void OnModelCreating(ModelBuilder modelBuilder)        {            modelBuilder.Entity<App>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Name).IsUnicode(false);                entity.HasOne(d => d.Tenant)                    .WithMany(p => p.Apps)                    .HasForeignKey(d => d.TenantId)                    .HasConstraintName("FK_TenantApps_Tenants");            });            modelBuilder.Entity<Client>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Address).IsUnicode(false);                entity.Property(e => e.Identifier).IsUnicode(false);                entity.Property(e => e.Secret).IsUnicode(false);            });            modelBuilder.Entity<Control>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.ControlType).IsUnicode(false);                entity.Property(e => e.Identifier).IsUnicode(false);                entity.HasOne(d => d.PageCategory)                    .WithMany(p => p.Controls)                    .HasForeignKey(d => d.PageCategoryId)                    .OnDelete(DeleteBehavior.Cascade)                    .HasConstraintName("FK_Controls_PageCategories");                entity.HasOne(d => d.ParentControlNavigation)                    .WithMany(p => p.InverseParentControlNavigation)                    .HasForeignKey(d => d.ParentControl)                    .HasConstraintName("FK_PageControls_PageControls");            });            modelBuilder.Entity<ControlValidator>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.HasOne(d => d.Control)                    .WithMany(p => p.ControlValidators)                    .HasForeignKey(d => d.ControlId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_ControlValidators_Controls");                entity.HasOne(d => d.Validator)                    .WithMany(p => p.ControlValidators)                    .HasForeignKey(d => d.ValidatorId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_ControlValidators_Validators");            });            modelBuilder.Entity<CustomField>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Name).IsUnicode(false);                entity.Property(e => e.Type).IsUnicode(false);                entity.HasOne(d => d.Page)                    .WithMany(p => p.CustomFields)                    .HasForeignKey(d => d.PageId)                    .HasConstraintName("FK_CustomFields_Pages");            });            modelBuilder.Entity<CustomText>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Code).IsUnicode(false);                entity.Property(e => e.Locale).IsUnicode(false);                entity.HasOne(d => d.Tenant)                    .WithMany(p => p.CustomTexts)                    .HasForeignKey(d => d.TenantId)                    .HasConstraintName("FK_CustomTexts_Tenants");            });            modelBuilder.Entity<Domain>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Chain).IsUnicode(false);                entity.Property(e => e.Name).IsUnicode(false);            });            modelBuilder.Entity<NavigationGroup>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();            });            modelBuilder.Entity<NavigationPage>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.HasOne(d => d.NavigationGroup)                    .WithMany(p => p.NavigationPages)                    .HasForeignKey(d => d.NavigationGroupId)                    .HasConstraintName("FK_NavigationPages_NavigationGroups");                entity.HasOne(d => d.Page)                    .WithMany(p => p.NavigationPages)                    .HasForeignKey(d => d.PageId)                    .OnDelete(DeleteBehavior.Cascade)                    .HasConstraintName("FK_NavigationPages_Pages");            });            modelBuilder.Entity<Page>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Apps).IsUnicode(false);                entity.Property(e => e.Layout).IsUnicode(false);                entity.Property(e => e.Name).IsUnicode(false);                entity.Property(e => e.PrivilegeType).IsUnicode(false);                entity.Property(e => e.RouteParameters).IsUnicode(false);                entity.Property(e => e.SpecialPermission).IsUnicode(false);                entity.Property(e => e.ViewPath).IsUnicode(false);                entity.HasOne(d => d.Domain)                    .WithMany(p => p.Pages)                    .HasForeignKey(d => d.DomainId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_Pages_Domains");                entity.HasOne(d => d.PageCategory)                    .WithMany(p => p.Pages)                    .HasForeignKey(d => d.PageCategoryId)                    .HasConstraintName("FK_Pages_PageCategories");                entity.HasOne(d => d.ResourceAction)                    .WithMany(p => p.Pages)                    .HasForeignKey(d => d.ResourceActionId)                    .HasConstraintName("FK_Pages_ResourceActions");                entity.HasOne(d => d.Resource)                    .WithMany(p => p.Pages)                    .HasForeignKey(d => d.ResourceId)                    .HasConstraintName("FK_Pages_Resources");                entity.HasOne(d => d.SourceCollection)                    .WithMany(p => p.Pages)                    .HasForeignKey(d => d.SourceCollectionId)                    .HasConstraintName("FK_Pages_DomainEntityCollections1");                entity.HasOne(d => d.Tenant)                    .WithMany(p => p.Pages)                    .HasForeignKey(d => d.TenantId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_Pages_Tenants");            });            modelBuilder.Entity<PageCategory>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.BaseComponent).IsUnicode(false);                entity.Property(e => e.Layout).IsUnicode(false);                entity.Property(e => e.Name).IsUnicode(false);                entity.Property(e => e.ViewPath).IsUnicode(false);                entity.HasOne(d => d.Domain)                    .WithMany(p => p.PageCategories)                    .HasForeignKey(d => d.DomainId)                    .HasConstraintName("FK_PageCategories_Domains");                entity.HasOne(d => d.Resource)                    .WithMany(p => p.PageCategories)                    .HasForeignKey(d => d.ResourceId)                    .HasConstraintName("FK_PageCategories_Resources");            });            modelBuilder.Entity<PageCategoryParameter>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.DefaultValue).IsUnicode(false);                entity.Property(e => e.Name).IsUnicode(false);                entity.HasOne(d => d.PageCategory)                    .WithMany(p => p.PageCategoryParameters)                    .HasForeignKey(d => d.PageCategoryId)                    .HasConstraintName("FK_PageCategoryParameters_PageCategories");            });            modelBuilder.Entity<PageControl>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.HasOne(d => d.Control)                    .WithMany(p => p.PageControls)                    .HasForeignKey(d => d.ControlId)                    .HasConstraintName("FK_PageControls_Controls");                entity.HasOne(d => d.Page)                    .WithMany(p => p.PageControls)                    .HasForeignKey(d => d.PageId)                    .HasConstraintName("FK_PageControls_Pages");                entity.HasOne(d => d.SourceCollection)                    .WithMany(p => p.PageControls)                    .HasForeignKey(d => d.SourceCollectionId)                    .HasConstraintName("FK_PageControls_DomainEntityCollections");            });            modelBuilder.Entity<PageControlValidator>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.HasOne(d => d.PageControl)                    .WithMany(p => p.PageControlValidators)                    .HasForeignKey(d => d.PageControlId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_PageControlValidators_PageControls");                entity.HasOne(d => d.Validator)                    .WithMany(p => p.PageControlValidators)                    .HasForeignKey(d => d.ValidatorId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_PageControlValidators_Validators");            });            modelBuilder.Entity<PageParameter>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.HasOne(d => d.PageCategoryParameter)                    .WithMany(p => p.PageParameters)                    .HasForeignKey(d => d.PageCategoryParameterId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_PageParameters_PageCategoryParameters");                entity.HasOne(d => d.Page)                    .WithMany(p => p.PageParameters)                    .HasForeignKey(d => d.PageId)                    .HasConstraintName("FK_PageParameters_Pages");            });            modelBuilder.Entity<PageRoute>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.HasOne(d => d.Page)                    .WithMany(p => p.PageRoutes)                    .HasForeignKey(d => d.PageId)                    .HasConstraintName("FK_PageRoutes_Pages");            });            modelBuilder.Entity<Resource>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Name).IsUnicode(false);                entity.Property(e => e.ServiceName).IsUnicode(false);                entity.HasOne(d => d.Domain)                    .WithMany(p => p.Resources)                    .HasForeignKey(d => d.DomainId)                    .HasConstraintName("FK_Resources_Domains");            });            modelBuilder.Entity<ResourceAction>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Name).IsUnicode(false);                entity.HasOne(d => d.Resource)                    .WithMany(p => p.ResourceActions)                    .HasForeignKey(d => d.ResourceId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_ResourceActions_Resources");                entity.HasOne(d => d.Tenant)                    .WithMany(p => p.ResourceActions)                    .HasForeignKey(d => d.TenantId)                    .OnDelete(DeleteBehavior.ClientSetNull)                    .HasConstraintName("FK_ResourceActions_Tenants");            });            modelBuilder.Entity<ResourceCollection>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Name).IsUnicode(false);                entity.HasOne(d => d.Resource)                    .WithMany(p => p.ResourceCollections)                    .HasForeignKey(d => d.ResourceId)                    .OnDelete(DeleteBehavior.Cascade)                    .HasConstraintName("FK_ResourceCollections_Resources");            });            modelBuilder.Entity<ResourceCollectionCondition>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Property).IsUnicode(false);                entity.HasOne(d => d.DomainEntityCollection)                    .WithMany(p => p.ResourceCollectionConditions)                    .HasForeignKey(d => d.DomainEntityCollectionId)                    .HasConstraintName("FK_EntityCollectionConditions_DomainEntityCollections");            });            modelBuilder.Entity<Tenant>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.Code).IsUnicode(false);                entity.Property(e => e.Version).IsUnicode(false);            });            modelBuilder.Entity<Validator>(entity =>            {                entity.Property(e => e.Id).ValueGeneratedNever();                entity.Property(e => e.CalendarType).IsUnicode(false);                entity.Property(e => e.MaxValue).IsUnicode(false);                entity.Property(e => e.MinValue).IsUnicode(false);                entity.Property(e => e.Pattern).IsUnicode(false);                entity.Property(e => e.Type).IsUnicode(false);            });        }    }}