using CodeShellCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Timers;
using System.Threading.Tasks;

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
        bool _firstRun = true;

        void _setJob(ITimedJob val)
        {
            _job = val;
            if (Timer != null)
            {
                Timer.Stop();
                Timer.Dispose();
            }
            _firstRun = true;
            Timer = new Timer();
            Timer.Elapsed += (s, e) =>
            {
                CheckFirstRun();
                RunJob();
            };

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
            if (_firstRun)
            {
                Timer.Interval = Job.Interval.TotalMilliseconds;
                _firstRun = false;
            }
        }

        public virtual async Task RunJob()
        {

            using (var sc = Shell.GetScope())
            {
                await Job.Run(sc.ServiceProvider);
            }
        }
    }
}
