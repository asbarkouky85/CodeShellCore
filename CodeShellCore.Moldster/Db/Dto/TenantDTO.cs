using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db.Dto
{
    public class TenantDTO
    {
        public long Id { get; set; }
        public string TenantCode { get; set; }
        public string Name { get; set; }
    }
}
