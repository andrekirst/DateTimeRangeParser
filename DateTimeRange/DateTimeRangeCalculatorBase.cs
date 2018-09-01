using System;
using System.Collections.Generic;

namespace DateTimeRange
{
    public abstract class DateTimeRangeCalculatorBase
    {
        public IDateTimeProvider DateTimeProvider { protected get; set; }

        public List<DateTimeRangeCalculatorBase> OtherCalculations { protected get; set; } = new List<DateTimeRangeCalculatorBase>();

        public virtual bool NeedsOtherCalculations { get; } = false;

        public abstract string Name { get; }

        public abstract bool DoesMatchInput(string input);

        public abstract DateTimeRange CalculateFromInput(string input = "");

        protected DateTime Today => DateTimeProvider.Today;

        protected bool EqualsLowerMatch(string input, string match)
            => input?.ToLower() == match?.ToLower();
    }
}