using CodeShellCore.Data.Auditing;
using System;

namespace CodeShellCore
{
    public class FullAuditedEntity<TPrime> : AuditedEntity<TPrime>, IFullAudited
    {
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
