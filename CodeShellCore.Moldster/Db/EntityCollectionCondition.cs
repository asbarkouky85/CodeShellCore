﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class EntityCollectionCondition : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public long Id { get; set; }
        public long? DomainEntityCollectionId { get; set; }
        [StringLength(100)]
        public string Property { get; set; }
        [StringLength(300)]
        public string Value { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }

        [ForeignKey("DomainEntityCollectionId")]
        [InverseProperty("EntityCollectionConditions")]
        public DomainEntityCollection DomainEntityCollection { get; set; }
    }
}
