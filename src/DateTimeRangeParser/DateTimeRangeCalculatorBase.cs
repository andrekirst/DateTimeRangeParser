using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace DateTimeRangeParser
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

        public abstract List<CultureInfo> SupportedCultures { get; }

        public override string ToString()
        {
            return $"{Name} - ({string.Join(separator: ", ", values: SupportedCultures.Select(selector: s => s.EnglishName))})";
        }
    }
}