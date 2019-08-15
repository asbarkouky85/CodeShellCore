﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class Tenant : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public Tenant()
        {
            Pages = new HashSet<Page>();
            ResourceActions = new HashSet<ResourceAction>();
            TenantApps = new HashSet<TenantApp>();
        }

        public long Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Code { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        [Column(TypeName = "text")]
        public string ConnectionString { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        [StringLength(300)]
        public string Logo { get; set; }
        [StringLength(300)]
        public string MainComponentBase { get; set; }

        [InverseProperty("Tenant")]
        public ICollection<Page> Pages { get; set; }
        [InverseProperty("Tenant")]
        public ICollection<ResourceAction> ResourceActions { get; set; }
        [InverseProperty("Tenant")]
        public ICollection<TenantApp> TenantApps { get; set; }
    }
}
