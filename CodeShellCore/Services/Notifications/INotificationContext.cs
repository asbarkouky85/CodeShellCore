using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Services.Notifications
{
    public interface INotificationContext<TNote> where TNote : class,INotifier
    {
        void Run(Func<TNote,Task> action);
    }
}
