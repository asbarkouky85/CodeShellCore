using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class TenantDomain : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public TenantDomain()
        {
            Pages = new HashSet<Page>();
        }

        public long Id { get; set; }
        public long TenantId { get; set; }
        public long DomainId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }

        [ForeignKey("DomainId")]
        [InverseProperty("TenantDomains")]
        public Domain Domain { get; set; }
        [ForeignKey("TenantId")]
        [InverseProperty("TenantDomains")]
        public Tenant Tenant { get; set; }
        [InverseProperty("TenantDomain")]
        public ICollection<Page> Pages { get; set; }
    }
}
