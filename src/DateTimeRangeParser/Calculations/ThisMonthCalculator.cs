using System;
using System.Collections.Generic;
using System.Globalization;

namespace DateTimeRangeParser.Calculations
{
    public class ThisMonthCalculator : DateTimeRangeCalculatorBase
    {
        public override string Name => "ThisMonth";

        public override List<CultureInfo> SupportedCultures =>
            new List<CultureInfo>()
            {
                CultureInfo.GetCultureInfoByIetfLanguageTag(name: "en")
            };

        public sealed override DateTimeRange CalculateFromInput(string input = "")
        {
            DateTime nextMonthSameDay = Today.AddMonths(months: 1);
            DateTime end = Today.AddMonths(months: 1).AddDays(value: -nextMonthSameDay.Day);

            return new DateTimeRange
            {
                Start = Today.AddDays(value: -Today.Day + 1),
                End = end
            };
        }

        public override bool DoesMatchInput(string input)
        {
            return EqualsLowerMatch(
                input: input,
                match: "thismonth");
        }

        public override IEnumerable<CalculationExample> Examples
        {
            get
            {
                yield return new CalculationExample { InputString = "thismonth" };
            }
        }
    }
}