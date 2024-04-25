using System;
using System.Threading.Tasks;

namespace CodeShellCore.Notifications
{
    public interface IEmitter<TNote> where TNote : class
    {
        void Emit(Func<TNote, Task> action, string[] only = null, string[] exclude = null);
    }
}
