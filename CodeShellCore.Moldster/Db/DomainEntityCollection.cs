﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class DomainEntityCollection : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public DomainEntityCollection()
        {
            EntityCollectionConditions = new HashSet<EntityCollectionCondition>();
            PageControls = new HashSet<PageControl>();
            Pages = new HashSet<Page>();
        }

        public long Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public long DomainEntityId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }

        [ForeignKey("DomainEntityId")]
        [InverseProperty("DomainEntityCollections")]
        public DomainEntity DomainEntity { get; set; }
        [InverseProperty("DomainEntityCollection")]
        public ICollection<EntityCollectionCondition> EntityCollectionConditions { get; set; }
        [InverseProperty("SourceCollection")]
        public ICollection<PageControl> PageControls { get; set; }
        [InverseProperty("SourceCollection")]
        public ICollection<Page> Pages { get; set; }
    }
}
