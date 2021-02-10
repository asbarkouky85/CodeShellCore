using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CodeShellCore.Helpers
{
    public class SW
    {
        private SW() { }

        public static TimeConsumption Measure(string logName = null)
        {
            return new TimeConsumption(logName);
        }
    }
}

