using System.Text.RegularExpressions;

namespace DateTimeRange.DateTimeRangeCalculators
{
    public class TodayCalculator : IDateTimeRangeCalculator
    {
        public IDateTimeProvider DateTimeProvider { get; set; }

        public string Name => "Today";

        public DateTimeRange CalculateFromInput(string input = "")
        {
            return new DateTimeRange
            {
                Start = DateTimeProvider.Today,
                End = DateTimeProvider.Today
            };
        }

        public bool DoesMatchInput(string input)
        {
            return Regex.IsMatch(
                input: input,
                pattern: @"[tT][oO][dD][aA][yY]",
                options: RegexOptions.Compiled);
        }
    }
}