using CodeShellCore.Services.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Http.Pushing
{
    public interface IMessagePusher<TNote> where TNote : class
    {
        void Publish(Func<TNote, Task> action, string[] only = null, string[] exclude = null);
    }
}
