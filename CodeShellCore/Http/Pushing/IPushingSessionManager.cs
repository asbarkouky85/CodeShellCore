using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeShellCore.Http.Pushing
{
    public interface IPushingSessionManager
    {
        string GetConnectionId();
    }
}
