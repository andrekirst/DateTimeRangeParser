using System;
using System.Collections.Generic;
using System.Globalization;

namespace DateTimeRangeParser.Calculations
{
    public class YesterdayCalculator : DateTimeRangeCalculatorBase
    {
        public override string Name => "Yesterday";

        public override List<CultureInfo> SupportedCultures =>
            new List<CultureInfo>()
            {
                CultureInfo.GetCultureInfoByIetfLanguageTag(name: "en")
            };

        public sealed override DateTimeRange CalculateFromInput(string input = "")
        {
            DateTime yesterday = DateTimeProvider.Today.AddDays(value: -1);

            return new DateTimeRange
            {
                Start = yesterday,
                End = yesterday
            };
        }

        public override bool DoesMatchInput(string input)
        {
            return EqualsLowerMatch(
                input: input,
                match: "yesterday");
        }
    }
}