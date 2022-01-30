using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security.Authentication
{
    public interface IRegisterModel
    {
        string LogonName { get; set; }
        string Password { get; set; }
        string PasswordConfim { get; set; }
    }
}
