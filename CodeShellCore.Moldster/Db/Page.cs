﻿using System;using System.Collections.Generic;using System.ComponentModel.DataAnnotations;using System.ComponentModel.DataAnnotations.Schema;namespace CodeShellCore.Moldster.Db{    public partial class Page : CodeShellCore.Moldster.Db.Data.MoldsterModelBase, CodeShellCore.Moldster.Db.Data.IMoldsterModel    {        public Page()        {            PageControls = new HashSet<PageControl>();        }        public long Id { get; set; }        [StringLength(50)]        public string Name { get; set; }        [StringLength(300)]        public string ViewPath { get; set; }        public string Apps { get; set; }        [Column(TypeName = "ntext")]        public string ViewParams { get; set; }        public long? ResourceId { get; set; }        [StringLength(200)]        public string Layout { get; set; }        [StringLength(50)]        public string PrivilegeType { get; set; }        public long? ResourceActionId { get; set; }        [StringLength(50)]        public string SpecialPermission { get; set; }        public long? SourceCollectionId { get; set; }        [StringLength(150)]        public string DisplayName { get; set; }        public long? PageCategoryId { get; set; }        [StringLength(100)]        public string RouteParameters { get; set; }        public bool AppearsInNavigation { get; set; }        public bool HasRoute { get; set; }        public bool CanEmbed { get; set; }        [Column(TypeName = "datetime")]        public DateTime? CreatedOn { get; set; }        [Column(TypeName = "datetime")]        public DateTime? UpdatedOn { get; set; }        public long? CreatedBy { get; set; }        public long? UpdatedBy { get; set; }        public int DefaultAccessibility { get; set; }        public bool? Presistant { get; set; }        public long DomainId { get; set; }        public long TenantId { get; set; }        [ForeignKey("DomainId")]        [InverseProperty("Pages")]		[System.Runtime.Serialization.IgnoreDataMember]
        public Domain Domain { get; set; }        [ForeignKey("PageCategoryId")]        [InverseProperty("Pages")]		[System.Runtime.Serialization.IgnoreDataMember]
        public PageCategory PageCategory { get; set; }        [ForeignKey("ResourceId")]        [InverseProperty("Pages")]		[System.Runtime.Serialization.IgnoreDataMember]
        public Resource Resource { get; set; }        [ForeignKey("ResourceActionId")]        [InverseProperty("Pages")]		[System.Runtime.Serialization.IgnoreDataMember]
        public ResourceAction ResourceAction { get; set; }        [ForeignKey("SourceCollectionId")]        [InverseProperty("Pages")]		[System.Runtime.Serialization.IgnoreDataMember]
        public DomainEntityCollection SourceCollection { get; set; }        [ForeignKey("TenantId")]        [InverseProperty("Pages")]		[System.Runtime.Serialization.IgnoreDataMember]
        public Tenant Tenant { get; set; }        [InverseProperty("Page")]        public ICollection<PageControl> PageControls { get; set; }    }}