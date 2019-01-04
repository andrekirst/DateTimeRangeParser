using System.Collections.Generic;
using System.Globalization;

namespace DateTimeRangeParser.Calculations
{
    public class TodayCalculator : DateTimeRangeCalculatorBase
    {
        public override string Name => "Today";

        public override List<CultureInfo> SupportedCultures =>
            new List<CultureInfo>()
            {
                CultureInfo.GetCultureInfoByIetfLanguageTag(name: "en")
            };

        public sealed override DateTimeRange CalculateFromInput(string input = "")
        {
            return new DateTimeRange
            {
                Start = Today,
                End = Today
            };
        }

        public override bool DoesMatchInput(string input)
        {
            return EqualsLowerMatch(
                input: input,
                match: "today");
        }

        public override IEnumerable<CalculationExample> Examples
        {
            get
            {
                yield return new CalculationExample { InputString = "today" };
            }
        }
    }
}