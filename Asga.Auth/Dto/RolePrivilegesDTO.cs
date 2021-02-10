using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Auth.Dto
{
    public class RolePrivilegesDTO
    {
        public UserRole UserRole { get; set; }
        public IEnumerable<RoleResource> Resources { get; set; }
        public IEnumerable<RoleResourceAction> ResourceActions { get; set; }

        public RolePrivilegesDTO()
        {
            Resources = new List<RoleResource>();
            ResourceActions = new List<RoleResourceAction>();
        }
    }
}
