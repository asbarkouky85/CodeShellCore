using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.HealthCheck
{
    public interface IHealthCheckUnit : IUnitOfWork
    {
        IRepository<CheckItem> CheckItemRepository { get; }
    }
}
