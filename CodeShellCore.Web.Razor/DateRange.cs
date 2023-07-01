using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeShellCore.Web.Razor
{
    public class DateRange
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Message { get; set; }

        public DateRange(string start = null, string end = null)
        {
            StartDate = start;
            EndDate = end;
        }

        public override string ToString()
        {
            return (StartDate == null ? "" : StartDate) + "," + (EndDate == null ? "" : EndDate + "");
        }
    }
}
