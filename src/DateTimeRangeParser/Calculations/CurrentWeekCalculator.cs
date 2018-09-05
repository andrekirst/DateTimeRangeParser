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
            DateTime today = DateTimeProvider.Today;

            DateTime start = today.AddDays(value: -(int)today.DayOfWeek + 1);
            DateTime end = today.AddDays(value: 6 - 7 % (int)today.DayOfWeek);

            return new DateTimeRange
            {
                Start = start,
                End = end
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