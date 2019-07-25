﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class DomainEntity : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public DomainEntity()
        {
            DomainEntityCollections = new HashSet<DomainEntityCollection>();
            DomainEntityPropertyDomainEntities = new HashSet<DomainEntityProperty>();
            DomainEntityPropertyReferenceEntities = new HashSet<DomainEntityProperty>();
            PageCategories = new HashSet<PageCategory>();
        }

        public long Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public long DomainId { get; set; }
        public bool IsSystem { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }

        [ForeignKey("DomainId")]
        [InverseProperty("DomainEntities")]
        public Domain Domain { get; set; }
        [InverseProperty("DomainEntity")]
        public ICollection<DomainEntityCollection> DomainEntityCollections { get; set; }
        [InverseProperty("DomainEntity")]
        public ICollection<DomainEntityProperty> DomainEntityPropertyDomainEntities { get; set; }
        [InverseProperty("ReferenceEntity")]
        public ICollection<DomainEntityProperty> DomainEntityPropertyReferenceEntities { get; set; }
        [InverseProperty("DomainEntity")]
        public ICollection<PageCategory> PageCategories { get; set; }
    }
}
