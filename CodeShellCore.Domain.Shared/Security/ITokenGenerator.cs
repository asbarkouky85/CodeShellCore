using CodeShellCore.Security.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security
{
    public interface ITokenGenerator
    {
        void SetToken(LoginResult res, string deviceId = null, bool remember = false);
    }
}
