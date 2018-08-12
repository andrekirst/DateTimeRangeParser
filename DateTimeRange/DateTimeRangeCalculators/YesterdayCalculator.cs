using System;

namespace DateTimeRange.DateTimeRangeCalculators
{
    public class YesterdayCalculator : DateTimeRangeCalculatorBase
    {
        public override string Name => "Yesterday";

        public override DateTimeRange CalculateFromInput(string input = "")
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
            return ToLowerInputMatch(
                input: input,
                match: "yesterday");
        }
    }
}