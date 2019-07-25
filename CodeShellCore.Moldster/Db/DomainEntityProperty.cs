using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class DomainEntityProperty : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public DomainEntityProperty()
        {
            Controls = new HashSet<Control>();
        }

        public long Id { get; set; }
        public long? DomainEntityId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public bool IsSystem { get; set; }
        [StringLength(50)]
        public string DataType { get; set; }
        public long? ReferenceEntityId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }

        [ForeignKey("DomainEntityId")]
        [InverseProperty("DomainEntityPropertyDomainEntities")]
        public DomainEntity DomainEntity { get; set; }
        [ForeignKey("ReferenceEntityId")]
        [InverseProperty("DomainEntityPropertyReferenceEntities")]
        public DomainEntity ReferenceEntity { get; set; }
        [InverseProperty("DomainEntityProperty")]
        public ICollection<Control> Controls { get; set; }
    }
}
