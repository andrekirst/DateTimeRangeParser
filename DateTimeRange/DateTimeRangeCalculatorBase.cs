using System;

namespace DateTimeRange
{
    public abstract class DateTimeRangeCalculatorBase
    {
        public IDateTimeProvider DateTimeProvider { get; set; }

        public abstract string Name { get; }

        public abstract bool DoesMatchInput(string input);

        public abstract DateTimeRange CalculateFromInput(string input = "");

        public DateTime Today => DateTimeProvider.Today;

        protected bool ToLowerInputMatch(string input, string match)
            => input?.ToLower() == match?.ToLower();
    }
}