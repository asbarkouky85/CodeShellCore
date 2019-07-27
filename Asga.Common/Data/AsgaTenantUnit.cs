using CodeShellCore.Security;
using Asga.Security;
using System;
using System.Collections.Generic;
using System.Text;
using Asga.Common;
using CodeShellCore;

namespace Asga.Common.Data
{

    public class AsgaTenantUnit<T> : AsgaUnitBase<T> where T : AsgaTenantContext
    {
        public AsgaTenantUnit(CurrentTenant data,IUserAccessor userAccessor):base(userAccessor)
        {
            DbContext.ConnectionString = Shell.GetConfigAs<string>("ConnectionStrings:Auth");
        }
    }
}
