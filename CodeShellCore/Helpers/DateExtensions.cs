using System;
using System.Collections.Generic;
using System.Text;

namespace CodeShellCore.Helpers
{
    public static class DateExtensions
    {
        public static DateTime GetDayStart(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }

        public static DateTime GetDayEnd(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }

        public static DateTime RemoveMilli(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
        }
    }
}
