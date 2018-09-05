using System.Linq;

namespace DateTimeRangeParser.Calculations
{
    public class DynamicRangeCalculator : DateTimeRangeCalculatorBase
    {
        private const string Separator = "->";

        public override string Name => "DynamicRange";

        public override DateTimeRange CalculateFromInput(string input = "")
        {
            string[] splitBySeperator = input.Split(separator: Separator);

            DateTimeRange startDateTimeRange =
                OtherCalculations
                .FirstOrDefault(predicate: m => m.DoesMatchInput(input: splitBySeperator.First()))
                    ?.CalculateFromInput(input: splitBySeperator.First());

            DateTimeRange endDateTimeRange =
                OtherCalculations
                .FirstOrDefault(predicate: m => m.DoesMatchInput(input: splitBySeperator.Last()))
                    ?.CalculateFromInput(input: splitBySeperator.Last());

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
    }
}