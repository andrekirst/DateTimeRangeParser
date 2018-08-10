namespace DateTimeRange
{
    public interface IDateTimeRangeCalculator
    {
        IDateTimeProvider DateTimeProvider { get; }

        string Name { get; }

        string DetectionRegexPattern { get; }

        DateTimeRange CalculateFromInput(string input = "");
    }
}