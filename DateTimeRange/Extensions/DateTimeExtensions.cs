using System;
using System.Collections.Generic;
using System.Text;

namespace DateTimeRange.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetFirstMondayOfYear(this DateTime dateTime)
        {
            while (dateTime.DayOfWeek != DayOfWeek.Monday)
            {
                dateTime = dateTime.AddDays(1);
            }
            return dateTime;
        }
    }
}
