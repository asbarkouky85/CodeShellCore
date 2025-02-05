using CodeShellCore.Data.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CodeShellCore
{
    public class AuditedEntity<TPrime> : Entity<TPrime>, IAudited
    {
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }

    
}
