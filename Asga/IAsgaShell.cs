using System.Collections.Generic;
using Asga.Security;

namespace Asga
{
    public interface IAsgaShell
    {
        Dictionary<long, TenantInfo> getConnectionStringsDictionary();
    }
}
