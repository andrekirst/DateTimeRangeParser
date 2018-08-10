namespace DateTimeRange.DateTimeRangeCalculators
{
    public class CalendarWeekCalculator : IDateTimeRangeCalculator
    {
        public CalendarWeekCalculator(IDateTimeProvider dateTimeProvider)
        {
            DateTimeProvider = dateTimeProvider;
        }

        public IDateTimeProvider DateTimeProvider { get; }

        public string Name => "CalendarWeek";

        public string DetectionRegexPattern => @"^CW([1-9]|1[0-9]|2[0-9]|3[0-9]|4[0-9]|5[0-3])\.[0-9]{4,4}$";

        public DateTimeRange CalculateFromInput(string input)
        {
            throw new System.NotImplementedException();
        }
    }
}