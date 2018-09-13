using System;
using System.Collections.Generic;
using System.Globalization;

namespace DateTimeRangeParser.Calculations
{
    public class CurrentWeekCalculator : DateTimeRangeCalculatorBase
    {
        public override string Name => "CurrentWeek";

        public override List<CultureInfo> SupportedCultures =>
            new List<CultureInfo>()
            {
                CultureInfo.GetCultureInfoByIetfLanguageTag(name: "en")
            };

        public sealed override DateTimeRange CalculateFromInput(string input = "")
        {
            DateTime start = Today.AddDays(value: -(int)Today.DayOfWeek + 1);

            return new DateTimeRange
            {
                Start = start,
                End = start.AddDays(6)
            };
        }

        public override bool DoesMatchInput(string input)
        {
            return EqualsLowerMatch(
                input: input,
                match: "currentweek");
        }
    }
}