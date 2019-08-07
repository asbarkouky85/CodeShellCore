using CodeShellCore.Security.Authorization;
using System.Collections.Generic;

namespace Asga.Security
{
    public class RoleCacheDto  
    
    {
        public string RoleTenantId { get; set; }
        public long RoleId { get; set; }
        public IEnumerable<ResourceActionV> ResourceActionVs { get; set; }
        public IEnumerable<ResourceV> ResourceVs { get; set; }
        
    }
}
