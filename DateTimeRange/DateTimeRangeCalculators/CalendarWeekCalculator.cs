using System;
using System.Globalization;
using System.Text.RegularExpressions;
using DateTimeRange.Extensions;

namespace DateTimeRange.DateTimeRangeCalculators
{
    public class CalendarWeekCalculator : DateTimeRangeCalculatorBase
    {
        public override string Name => "CalendarWeek";

        public override DateTimeRange CalculateFromInput(string input = "")
        {
            string[] splitByPoint = input?.Split('.');
            int weekNumber = int.Parse(s: splitByPoint[0].Substring(2));
            int yearNumber = int.Parse(s: splitByPoint[1]);

            DateTime firstDayOfYear = new DateTime(yearNumber, 1, 1);

            DateTime firstMonday = firstDayOfYear.GetFirstMondayOfYear();
            DateTime firstMondayPlusCountOfWeeks = firstMonday.AddDays(7 * (weekNumber - 1));

            return new DateTimeRange
            {
                Start = firstMondayPlusCountOfWeeks,
                End = firstMondayPlusCountOfWeeks.AddDays(6)
            };
        }

        public override bool DoesMatchInput(string input)
        {
            return Regex.IsMatch(
                input: input,
                pattern: @"^CW([1-9]|1[0-9]|2[0-9]|3[0-9]|4[0-9]|5[0-3])\.[0-9]{4,4}$",
                options: RegexOptions.Compiled);
        }
    }
}