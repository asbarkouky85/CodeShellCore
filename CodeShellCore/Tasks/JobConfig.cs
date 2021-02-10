using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Tasks
{
    public class JobConfig
    {
        public IEnumerable<ITimedJob> Jobs { get; private set; }
        public JobConfig(IEnumerable<ITimedJob> jobs)
        {
            Jobs = jobs;
        }
    }
}
