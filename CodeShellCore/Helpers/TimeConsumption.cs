using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CodeShellCore.Helpers
{
    public class TimeConsumption : IDisposable
    {
        Stopwatch Watch;
        string logName;
        public TimeSpan Elapsed { get { return Watch.Elapsed; } }
        public TimeConsumption(string name = null)
        {
            Watch = new Stopwatch();
            logName = name;
            Watch.Start();
        }

        public void Dispose()
        {
            Watch.Stop();
            if (logName != null)
                Console.WriteLine(logName + " - " + Watch.Elapsed.TotalSeconds.ToString("F4"));

            Watch.Reset();
        }
    }
}
