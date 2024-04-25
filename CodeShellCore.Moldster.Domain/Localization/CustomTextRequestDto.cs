using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Localization
{
   public class CustomTextRequest
    {
        public long TenantId { get; set; }
        public int Type { get; set; }
        public string Locale { get; set; }
        public bool ModifiedOnly { get; set; }
    }
}
