using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Pages
{
    public class CustomFieldDto : EntityDto<long>, IDetailObject<long>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public long PageId { get; set; }
        public string State { get; set; }

    }
}
