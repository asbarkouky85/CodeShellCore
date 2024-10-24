using CodeShellCore.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Tasks
{
    public interface ITimedJob
    {
        bool RunOnStartUp { get; }
        TimeOfDay? StartOn { get; }
        TimeSpan Interval { get; }
        Task<SubmitResult> Run(IServiceProvider provider);
    }
}
