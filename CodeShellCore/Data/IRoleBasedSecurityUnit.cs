using System;
using System.Collections.Generic;
using System.Text;
using CodeShellCore.Security;

namespace CodeShellCore.Data
{
   public interface IRoleBasedSecurityUnit : ISecurityUnit
   {
       IRoleRepository SecurityRoleRepository { get; }
   }
}
