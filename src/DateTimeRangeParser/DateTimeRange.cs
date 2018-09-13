using System;

namespace DateTimeRangeParser
{
    public class DateTimeRange
    {
        public DateTimeRange()
        {
        }

        public DateTimeRange(
            DateTime start,
            DateTime end)
        {
            Start = start;
            End = end;
        }

        public static DateTimeRange Empty =>
            new DateTimeRange(
                start: DateTime.MinValue,
                end: DateTime.MaxValue)
            {
                IsValid = false
            };

        public bool IsValid { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public override string ToString()
        {
            return $"Start: {Start} - End: {End}";
        }

        public override bool Equals(object obj)
        {
            DateTimeRange other = obj as DateTimeRange;

            return Start == other?.Start &&
                   End == other.End;
        }
    }
}