using System;
using System.Text.RegularExpressions;
using DateTimeRange.Extensions;

namespace DateTimeRange.DateTimeRangeCalculators
{
    public class CalendarWeekCalculator : DateTimeRangeCalculatorBase
    {
        public override string Name => "CalendarWeek";

        public override DateTimeRange CalculateFromInput(string input = "")
        {
            string[] splitByPoint = input?.Split(separator: '.');
            int weekNumber = int.Parse(s: splitByPoint?[0].Substring(startIndex: 2));
            int yearNumber = int.Parse(s: splitByPoint?[1]);

            DateTime firstDayOfYear = new DateTime(year: yearNumber, month: 1, day: 1);

            DateTime firstMonday = firstDayOfYear.GetFirstMondayOfYear();
            DateTime firstMondayPlusCountOfWeeks = firstMonday.AddDays(value: 7 * (weekNumber - 1));

            return new DateTimeRange
            {
                Start = firstMondayPlusCountOfWeeks,
                End = firstMondayPlusCountOfWeeks.AddDays(value: 6)
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