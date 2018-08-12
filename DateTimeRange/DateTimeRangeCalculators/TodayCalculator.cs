using System.Text.RegularExpressions;

namespace DateTimeRange.DateTimeRangeCalculators
{
    public class TodayCalculator : DateTimeRangeCalculatorBase
    {
        public override string Name => "Today";

        public override DateTimeRange CalculateFromInput(string input = "")
        {
            return new DateTimeRange
            {
                Start = DateTimeProvider.Today,
                End = DateTimeProvider.Today
            };
        }

        public override bool DoesMatchInput(string input)
        {
            return ToLowerInputMatch(
                input: input,
                match: "today");
        }
    }
}