using CodeShellCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Timers;

namespace CodeShellCore.Tasks
{
    public class JobRunner : IJobRunner
    {
        private ITimedJob _job;
        public ITimedJob Job
        {
            get { return _job; }
            set
            {
                _setJob(value);
            }
        }
        public Timer Timer { get; private set; }
        bool fistRun = true;

        void _setJob(ITimedJob val)
        {
            _job = val;
            if (Timer != null)
            {
                Timer.Stop();
                Timer.Dispose();
            }
            fistRun = true;
            Timer = new Timer();
            Timer.Elapsed += (s, e) => RunJob();
            if (_job.StartOn != null)
            {
                Timer.Interval = _getFirstInterval(_job.StartOn.Value);
            }
            else
            {
                Timer.Interval = Job.Interval.TotalMilliseconds;
            }
        }

        double _getFirstInterval(TimeOfDay dt)
        {
            var now = DateTime.Now;

            DateTime nDate = new DateTime(now.Year, now.Month, now.Day, dt.Hour, dt.Minute, dt.Second);
            if (nDate < now)
                nDate = nDate.AddDays(1);
            return (nDate - DateTime.Now).TotalMilliseconds;
        }

        protected virtual void CheckFirstRun()
        {
            if (fistRun)
            {
                Timer.Interval = Job.Interval.TotalMilliseconds;
                fistRun = false;
            }
        }

        protected virtual void RunJob()
        {
            CheckFirstRun();
            using (var sc = Shell.GetScope())
            {
                Job.Run(sc.ServiceProvider);
            }
        }
    }
}
