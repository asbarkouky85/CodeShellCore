using CodeShellCore.Data;
using CodeShellCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CodeShellCore.Moldster.Navigation
{
    [EntityName("NavigationPage")]
    public class NavigationPageListDTO : EntityDto<long>
    {
        public string Url { get; set; }
        public long TenantId { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public long? PageId { get; set; }
        public long NavigationGroupId { get; set; }

    }
}
