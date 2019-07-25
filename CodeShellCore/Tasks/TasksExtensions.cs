using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Tasks
{
    public static class TasksExtensions
    {
        public static T GetTaskResult<T>(this Task<T> tsk)
        {
            Task.WaitAll(tsk);
            return tsk.Result;
        }
    }
}
