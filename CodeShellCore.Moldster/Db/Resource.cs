﻿using System;using System.Collections.Generic;using System.ComponentModel.DataAnnotations;using System.ComponentModel.DataAnnotations.Schema;namespace CodeShellCore.Moldster{    public partial class Resource : MoldsterModelBase, IMoldsterModel    {        public Resource()        {            PageCategories = new HashSet<PageCategory>();            Pages = new HashSet<Page>();            ResourceActions = new HashSet<ResourceAction>();            ResourceCollections = new HashSet<ResourceCollection>();        }        public long Id { get; set; }        [StringLength(150)]        public string Name { get; set; }        [Column(TypeName = "datetime")]        public DateTime? CreatedOn { get; set; }        [Column(TypeName = "datetime")]        public DateTime? UpdatedOn { get; set; }        public long? CreatedBy { get; set; }        public long? UpdatedBy { get; set; }        [StringLength(50)]        public string ServiceName { get; set; }        public long? DomainId { get; set; }        [ForeignKey("DomainId")]        [InverseProperty("Resources")]		[System.Runtime.Serialization.IgnoreDataMember]
        public Domain Domain { get; set; }        [InverseProperty("Resource")]        public ICollection<PageCategory> PageCategories { get; set; }        [InverseProperty("Resource")]        public ICollection<Page> Pages { get; set; }        [InverseProperty("Resource")]        public ICollection<ResourceAction> ResourceActions { get; set; }        [InverseProperty("Resource")]        public ICollection<ResourceCollection> ResourceCollections { get; set; }    }}