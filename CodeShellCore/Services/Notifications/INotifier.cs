using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Services.Notifications
{
    public interface INotifier
    { }
    public interface INotifier<T>: INotifier where T : class
    {
        void SendMessage(T message);
    }
}
