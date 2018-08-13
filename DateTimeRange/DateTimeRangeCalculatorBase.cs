using System;

namespace DateTimeRange
{
    public abstract class DateTimeRangeCalculatorBase
    {
        public IDateTimeProvider DateTimeProvider { protected get; set; }

        public abstract string Name { get; }

        public abstract bool DoesMatchInput(string input);

        public abstract DateTimeRange CalculateFromInput(string input = "");

        protected DateTime Today => DateTimeProvider.Today;

        protected bool EqualsLowerMatch(string input, string match)
            => input?.ToLower() == match?.ToLower();
    }
}