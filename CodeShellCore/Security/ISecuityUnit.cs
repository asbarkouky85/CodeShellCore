using CodeShellCore.Data;
using CodeShellCore.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Security
{
    public interface ISecurityUnit : IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IResourceRepository ResourceRepository { get; }
    }
}
