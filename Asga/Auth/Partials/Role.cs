using CodeShellCore.MQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asga.Auth
{
    public partial class Role : ISharedModel
    {
        public string[] GetNavPropertyNames()
        {
            return new string[] { "TenantDomain" };
        }
    }
}
