using System;

namespace DateTimeRangeParser.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetFirstMondayOfYear(this DateTime dateTime)
        {
            while (dateTime.DayOfWeek != DayOfWeek.Monday)
            {
                dateTime = dateTime.AddDays(value: 1);
            }
            return dateTime;
        }
    }
}
