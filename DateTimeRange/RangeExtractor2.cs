using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DateTimeRange
{
    public class RangeExtractor2
    {
        private readonly List<IDateTimeRangeCalculator> _calculators =
            new List<IDateTimeRangeCalculator>();
        private readonly IDateTimeProvider _dateTimeSource;

        public RangeExtractor2(
            IDateTimeProvider dateTimeSource,
            IEnumerable<IDateTimeRangeCalculator> calculators)
        {
            _dateTimeSource = dateTimeSource;
            AddCalculators(calculators: calculators);
        }

        private void AddCalculators(IEnumerable<IDateTimeRangeCalculator> calculators)
        {
            _calculators.AddRange(collection: calculators);
        }

        public DateTimeRange GenerateDateTimeRangeFromInput(string input)
        {
            return _calculators
                .FirstOrDefault(predicate: c => Regex.IsMatch(
                    input: input,
                    pattern: c.DetectionRegexPattern))?
                .CalculateFromInput(input: input);
        }

        public IReadOnlyCollection<string> ImplementedCalculatorNames
            => _calculators
                .Select(selector: s => s.Name)
                .ToList()
                .AsReadOnly();
    }
}