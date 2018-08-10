using System.Text.RegularExpressions;

namespace DateTimeRange.DateTimeRangeCalculators
{
    public class CalendarWeekCalculator : IDateTimeRangeCalculator
    {
        public IDateTimeProvider DateTimeProvider { get; set; }

        public string Name => "CalendarWeek";

        public DateTimeRange CalculateFromInput(string input)
        {
            throw new System.NotImplementedException();
        }

        public bool DoesMatchInput(string input)
        {
            return Regex.IsMatch(
                input: input,
                pattern: @"^CW([1-9]|1[0-9]|2[0-9]|3[0-9]|4[0-9]|5[0-3])\.[0-9]{4,4}$",
                options: RegexOptions.Compiled);
        }
    }
}