using System.Linq;
using System.Collections.Generic;

namespace DateTimeRange
{
    public class RangeExtractor
    {
        private readonly List<DateTimeRangeCalculatorBase> _calculators =
            new List<DateTimeRangeCalculatorBase>();
        private readonly IDateTimeProvider _dateTimeProvider;

        public RangeExtractor(
            IDateTimeProvider dateTimeProvider,
            IEnumerable<DateTimeRangeCalculatorBase> calculators)
        {
            _dateTimeProvider = dateTimeProvider;
            AddCalculators(calculators: calculators);
        }

        private void AddCalculators(IEnumerable<DateTimeRangeCalculatorBase> calculators)
        {
            foreach (DateTimeRangeCalculatorBase calculator in calculators)
            {
                calculator.DateTimeProvider = _dateTimeProvider;
            }
            _calculators.AddRange(collection: calculators);
        }

        public DateTimeRange GenerateDateTimeRangeFromInput(string input)
        {
            return _calculators
                ?.FirstOrDefault(predicate: c => c.DoesMatchInput(input: input))
                ?.CalculateFromInput(input: input);
        }

        public IReadOnlyCollection<string> ImplementedCalculatorNames
            => _calculators
                .Select(selector: s => s.Name)
                .ToList()
                .AsReadOnly();
    }
}