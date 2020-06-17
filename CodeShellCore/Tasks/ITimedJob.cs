using CodeShellCore.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Tasks
{
    public interface ITimedJob
    {
        TimeOfDay? StartOn { get; }
        TimeSpan Interval { get; }
        SubmitResult Run(IServiceProvider provider);
    }
}
