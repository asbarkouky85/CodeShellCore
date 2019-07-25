﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class ResourceAction : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public ResourceAction()
        {
            Pages = new HashSet<Page>();
        }

        public long Id { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        public long ResourceId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long TenantId { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }

        [ForeignKey("ResourceId")]
        [InverseProperty("ResourceActions")]
        public Resource Resource { get; set; }
        [ForeignKey("TenantId")]
        [InverseProperty("ResourceActions")]
        public Tenant Tenant { get; set; }
        [InverseProperty("ResourceAction")]
        public ICollection<Page> Pages { get; set; }
    }
}
