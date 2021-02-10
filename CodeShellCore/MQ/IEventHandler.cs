using CodeShellCore.Data.Helpers;
using CodeShellCore.MQ.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.MQ
{
    public interface IEventHandler
    {

        Task Handle<T>(T item) where T : class;
    }
}
