namespace DateTimeRange.DateTimeRangeCalculators
{
    public class TodayCalculator : IDateTimeRangeCalculator
    {
        public TodayCalculator(IDateTimeProvider dateTimeProvider)
        {
            DateTimeProvider = dateTimeProvider;
        }

        public IDateTimeProvider DateTimeProvider { get; }

        public string Name => "Today";

        public string DetectionRegexPattern => "[tT][oO][dD][aA][yY]";

        public DateTimeRange CalculateFromInput(string input = "")
        {
            return new DateTimeRange
            {
                Start = DateTimeProvider.Today,
                End = DateTimeProvider.Today
            };
        }
    }
}