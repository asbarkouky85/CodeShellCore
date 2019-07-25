using CodeShellCore.MQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Moldster.Db
{
    public partial class Resource : ISharedModel
    {
        public string[] GetNavPropertyNames()
        {
            return new string[] { "Domain" };
        }
    }
    public partial class Domain : ISharedModel
    {
        public string[] GetNavPropertyNames()
        {
            return null;
        }
    }
    public partial class TenantDomain : ISharedModel
    {
        public string[] GetNavPropertyNames()
        {
            return new string[] { "Domain", "Tenant" };
        }
    }
    public partial class ResourceAction : ISharedModel
    {
        public string[] GetNavPropertyNames()
        {
            return new string[] { "Resource"};
        }
    }
}
