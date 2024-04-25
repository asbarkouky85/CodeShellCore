using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodeShellCore.Moldster.Localization
{
    public class CustomTextDto : EntityDto<long>, IDetailObject<long>
    {
        public string Code { get; set; }
        [StringLength(50)]
        public string Value { get; set; }
        [StringLength(5)]
        public string Locale { get; set; }
        public long TenantId { get; set; }
        public int Type { get; set; }
        public string State { get; set; }
    }
}
