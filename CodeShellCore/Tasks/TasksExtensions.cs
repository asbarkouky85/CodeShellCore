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

        public static Task Then(this Task task, Action ac)
        {
            var s = task.GetAwaiter();
            s.OnCompleted(ac);
            
            if (task.Status < TaskStatus.Running)
                task.Start();

            return task;
        }

        public static Task<T> Then<T>(this Task<T> task, Action<T> func)
        {
            task.Then(() =>
            {
                func.Invoke(task.Result);
            });
            return task;
        }
    }
}
