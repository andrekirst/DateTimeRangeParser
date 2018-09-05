using System;
using System.Collections.Generic;
using System.Globalization;

namespace DateTimeRangeParser.Calculations
{
    public class LastWeekCalculator : DateTimeRangeCalculatorBase
    {
        public override string Name => "LastWeek";

        public override List<CultureInfo> SupportedCultures =>
            new List<CultureInfo>()
            {
                CultureInfo.GetCultureInfoByIetfLanguageTag(name: "en")
            };

        public sealed override DateTimeRange CalculateFromInput(string input = "")
        {
            DateTime start = Today.AddDays(value: -(int)Today.DayOfWeek + 1 - 7);
            DateTime end = Today.AddDays(value: 7 % (int)Today.DayOfWeek - 7);

            return new DateTimeRange
            {
                Start = start,
                End = end
            };
        }

        public override bool DoesMatchInput(string input)
        {
            return EqualsLowerMatch(input: input, match: "lastweek");
        }
    }
}