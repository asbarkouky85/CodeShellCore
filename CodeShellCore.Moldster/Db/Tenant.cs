using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class Tenant : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public Tenant()
        {
            ResourceActions = new HashSet<ResourceAction>();
            TenantApps = new HashSet<TenantApp>();
            TenantDomains = new HashSet<TenantDomain>();
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
        public string MainComponentBase { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool IsActive { get; set; }

        [InverseProperty("Tenant")]
        public ICollection<ResourceAction> ResourceActions { get; set; }
        [InverseProperty("Tenant")]
        public ICollection<TenantApp> TenantApps { get; set; }
        [InverseProperty("Tenant")]
        public ICollection<TenantDomain> TenantDomains { get; set; }
    }
}
