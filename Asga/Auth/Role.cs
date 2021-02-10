﻿using System;using System.Collections.Generic;using System.ComponentModel.DataAnnotations;using System.ComponentModel.DataAnnotations.Schema;namespace Asga.Auth{    [Table("Roles", Schema = "Auth")]    public partial class Role : AsgaModelBase, IAsgaModel    {        public Role()        {            RoleResourceActions = new HashSet<RoleResourceAction>();            RoleResources = new HashSet<RoleResource>();            UserRoles = new HashSet<UserRole>();        }        public long Id { get; set; }        [StringLength(150)]        public string Name { get; set; }        public long? TenantDomainId { get; set; }        [StringLength(300)]        public string Description { get; set; }        [Column(TypeName = "datetime")]        public DateTime? CreatedOn { get; set; }        [Column(TypeName = "datetime")]        public DateTime? UpdatedOn { get; set; }        public long? CreatedBy { get; set; }        public long? UpdatedBy { get; set; }        public bool IsUserRole { get; set; }        public long? TenantAppId { get; set; }        [ForeignKey("TenantAppId")]        [InverseProperty("Roles")]		[System.Runtime.Serialization.IgnoreDataMember]        public App TenantApp { get; set; }        [InverseProperty("Role")]        public ICollection<RoleResourceAction> RoleResourceActions { get; set; }        [InverseProperty("Role")]        public ICollection<RoleResource> RoleResources { get; set; }        [InverseProperty("Role")]        public ICollection<UserRole> UserRoles { get; set; }    }}