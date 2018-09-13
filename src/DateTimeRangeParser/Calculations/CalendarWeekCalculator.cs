using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using DateTimeRangeParser.Extensions;

namespace DateTimeRangeParser.Calculations
{
    public class CalendarWeekCalculator : DateTimeRangeCalculatorBase
    {
        public override string Name => "CalendarWeek";

        public override List<CultureInfo> SupportedCultures =>
            new List<CultureInfo>()
            {
                CultureInfo.GetCultureInfoByIetfLanguageTag(name: "en")
            };

        public override DateTimeRange CalculateFromInput(string input = "")
        {
            string[] splitByPoint = input?.Split(separator: '.');
            int weekNumber = ExtractWeeknumber(splitByPoint: splitByPoint?[0]);
            int yearNumber = ExtractYearnumber(splitByPoint: splitByPoint?[1]);

            DateTime firstDayOfYear = new DateTime(year: yearNumber, month: 1, day: 1);
            DateTime firstMonday = firstDayOfYear.GetFirstMondayOfYear();

            DateTime firstMondayPlusCountOfWeeks = firstMonday.AddDays(value: 7 * (weekNumber - 1));

            return new DateTimeRange
            {
                Start = firstMondayPlusCountOfWeeks,
                End = firstMondayPlusCountOfWeeks.AddDays(value: 6)
            };
        }

        private static int ExtractYearnumber(string splitByPoint)
        {
            return int.Parse(s: splitByPoint);
        }

        private static int ExtractWeeknumber(string splitByPoint)
        {
            return int.Parse(s: splitByPoint.Substring(startIndex: 2));
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