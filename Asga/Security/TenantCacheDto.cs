using System;

namespace Asga.Security
{
    public class TenantCacheDto
    {
        public long TenantId { get; set; }
        public bool IsSync { get; set; }
        public DateTime LastUpdate { get; set; } = Convert.ToDateTime("01 / 01 / 2000");
    }
}
