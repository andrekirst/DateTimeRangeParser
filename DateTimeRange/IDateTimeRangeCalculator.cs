namespace DateTimeRange
{
    public interface IDateTimeRangeCalculator
    {
        IDateTimeProvider DateTimeProvider { get; set; }

        string Name { get; }

        bool DoesMatchInput(string input);

        DateTimeRange CalculateFromInput(string input = "");
    }
}