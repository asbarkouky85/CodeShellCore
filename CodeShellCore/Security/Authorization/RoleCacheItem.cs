using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security.Authorization
{
    public class RoleCacheItem
    {
        public object RoleId { get; set; }
        public IEnumerable<ResourceActionV> Actions { get; set; }
       // public IEnumerable<ResourceV> Resources { get; set; }
        public Dictionary<string, int> Resources { get; set; }
        public Dictionary<string, string> Collections { get; set; }
    }
}
