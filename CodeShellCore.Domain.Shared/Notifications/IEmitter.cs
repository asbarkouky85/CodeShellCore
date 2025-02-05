using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications
{
    public interface IEmitter<TContract> where TContract : class
    {
        void Emit(Expression<Func<TContract, Task>> action, string[] only = null, string[] exclude = null);
        Task EmitAsync(Expression<Func<TContract, Task>> action, string[] only = null, string[] exclude = null);
    }
}
