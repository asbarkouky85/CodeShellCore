using System;
using System.Collections.Generic;
using Asga.Auth.Views;

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
