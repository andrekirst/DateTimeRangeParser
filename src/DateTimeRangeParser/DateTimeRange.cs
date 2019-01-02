using System;
using System.Collections;
using System.Collections.Generic;

namespace DateTimeRangeParser
{
    public class DateTimeRange :
        IEquatable<DateTimeRange>,
        IEnumerable<DateTime>
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

        public bool IsValid { get; protected set; }

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

        public bool Equals(DateTimeRange other)
        {
            return other != null &&
                   IsValid == other.IsValid &&
                   Start == other.Start &&
                   End == other.End;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsValid, Start, End);
        }

        protected IEnumerable<DateTime> EnumerateFromStartToEnd()
        {
            if (End < Start)
            {
                yield break;
            }
            DateTime current = Start;
            while (current <= End)
            {
                yield return current;
                current = current.AddDays(value: 1);
            }
            yield break;
        }

        public IEnumerator<DateTime> GetEnumerator()
        {
            return EnumerateFromStartToEnd().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static bool operator ==(DateTimeRange range1, DateTimeRange range2)
        {
            return EqualityComparer<DateTimeRange>.Default.Equals(range1, range2);
        }

        public static bool operator !=(DateTimeRange range1, DateTimeRange range2)
        {
            return !(range1 == range2);
        }
    }
}