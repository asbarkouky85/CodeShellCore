using CodeShellCore.Data;
using CodeShellCore.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodeShellCore.Moldster.Tenants
{
    public class TenantDto : EntityDto<long>
    {
        [Required]
        [StringLength(100)]
        public string Code { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public string ConnectionString { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        [StringLength(300)]
        public string Logo { get; set; }
        [StringLength(300)]
        public string MainComponentBase { get; set; }
        [StringLength(100)]
        public string BaseStyle { get; set; }
        [StringLength(100)]
        public string Version { get; set; }
        public long? ParentId { get; set; }
        public bool UseParentUI { get; set; }

        public TmpFileData LogoFile { get; set; }
    }
}
