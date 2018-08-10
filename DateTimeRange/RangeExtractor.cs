using System.Linq;
using System.Collections.Generic;

namespace DateTimeRange
{
    public class RangeExtractor
    {
        private readonly List<IDateTimeRangeCalculator> _calculators =
            new List<IDateTimeRangeCalculator>();
        private readonly IDateTimeProvider _dateTimeProvider;

        public RangeExtractor(
            IDateTimeProvider dateTimeProvider,
            IEnumerable<IDateTimeRangeCalculator> calculators)
        {
            _dateTimeProvider = dateTimeProvider;
            AddCalculators(calculators: calculators);
        }

        private void AddCalculators(IEnumerable<IDateTimeRangeCalculator> calculators)
        {
            foreach (IDateTimeRangeCalculator calculator in calculators)
            {
                calculator.DateTimeProvider = _dateTimeProvider;
            }
            _calculators.AddRange(collection: calculators);
        }

        public DateTimeRange GenerateDateTimeRangeFromInput(string input)
        {
            return _calculators
                .FirstOrDefault(predicate: c => c.DoesMatchInput(input: input))
                .CalculateFromInput(input: input);
        }

        public IReadOnlyCollection<string> ImplementedCalculatorNames
            => _calculators
                .Select(selector: s => s.Name)
                .ToList()
                .AsReadOnly();
    }
}