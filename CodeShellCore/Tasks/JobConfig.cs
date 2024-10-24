using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Tasks
{
    public class JobConfig
    {
        private List<ITimedJob> _jobs = new List<ITimedJob>();
        public IReadOnlyCollection<ITimedJob> Jobs => _jobs;
        public JobConfig()
        {
        }

        public void AddJobs(params ITimedJob[] jobs)
        {
            _jobs.AddRange(jobs);
        }
    }
}
