﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeShellCore.Moldster.Db
{
    public partial class TenantApp : Data.MoldsterModelBase, Data.IMoldsterModel
    {
        public long Id { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(150)]
        public string DisplayName { get; set; }
        public long TenantId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? UpdatedBy { get; set; }

        [ForeignKey("TenantId")]
        [InverseProperty("TenantApps")]
        public Tenant Tenant { get; set; }
    }
}
