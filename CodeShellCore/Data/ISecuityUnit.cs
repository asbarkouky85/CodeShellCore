using CodeShellCore.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Data
{
    public interface ISecurityUnit : IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IResourceRepository ResourceRepository { get; }
    }
}
