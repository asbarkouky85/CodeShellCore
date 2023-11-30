using CodeShellCore.Data;
using CodeShellCore.Files;
using CodeShellCore.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodeShellCore.Moldster.Tenants
{
    [EntityName("Tenant")]
    public class TenantDto : EntityDto<long>
    {
        [Required]
        [StringLength(100)]
        public string Code { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        [StringLength(300)]
        public string MainComponentBase { get; set; }
        [StringLength(100)]
        public string BaseStyle { get; set; }
        [StringLength(100)]
        public string Version { get; set; }
        public long? ParentId { get; set; }
        public bool UseParentUI { get; set; }

        public TempFileDto LogoFile { get; set; }
    }
}
