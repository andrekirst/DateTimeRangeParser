using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DateTimeRangeParser.Calculations
{
    public sealed class DynamicRangeCalculator : DateTimeRangeCalculatorBase
    {
        private const string Separator = "->";

        public override string Name => "DynamicRange";

        public override DateTimeRange CalculateFromInput(string input = "")
        {
            string[] splitBySeperator = input
                .Split(separator: Separator)
                .Take(count: 2)
                .ToArray();

            string startInput = splitBySeperator.First();
            string endInput = splitBySeperator.Last();

            DateTimeRange startDateTimeRange =
                OtherCalculations
                    .FirstOrDefault(predicate: m => m.DoesMatchInput(input: startInput))
                    ?.CalculateFromInput(input: startInput) ?? DateTimeRange.Empty;

            DateTimeRange endDateTimeRange =
                OtherCalculations
                    .FirstOrDefault(predicate: m => m.DoesMatchInput(input: endInput))
                    ?.CalculateFromInput(input: endInput) ?? DateTimeRange.Empty;

            return new DateTimeRange(
                start: startDateTimeRange.Start,
                end: endDateTimeRange.End);
        }

        public override bool DoesMatchInput(string input)
        {
            string[] splitBySeperator = input.Split(separator: Separator);
            return
                OtherCalculations.Any(predicate: m => m.DoesMatchInput(input: splitBySeperator.First())) &&
                OtherCalculations.Any(predicate: m => m.DoesMatchInput(input: splitBySeperator.Last()));
        }

        public override bool NeedsOtherCalculations => true;

        public override List<CultureInfo> SupportedCultures => null;
    }
}