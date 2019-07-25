using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class Domain : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public Domain()
        {
            DomainEntities = new HashSet<DomainEntity>();
            Resources = new HashSet<Resource>();
            TenantDomains = new HashSet<TenantDomain>();
        }

        public long Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }

        [InverseProperty("Domain")]
        public ICollection<DomainEntity> DomainEntities { get; set; }
        [InverseProperty("Domain")]
        public ICollection<Resource> Resources { get; set; }
        [InverseProperty("Domain")]
        public ICollection<TenantDomain> TenantDomains { get; set; }
    }
}
