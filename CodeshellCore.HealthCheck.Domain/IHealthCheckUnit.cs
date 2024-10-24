using CodeShellCore.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.HealthCheck
{
    public interface IHealthCheckUnit : IUnitOfWork
    {
        IRepository<CheckItem> CheckItemRepository { get; }

        Task CleanupOldData();
    }
}
