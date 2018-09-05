using System.Linq;

namespace DateTimeRangeParser.Calculations
{
    public class DynamicRangeCalculator : DateTimeRangeCalculatorBase
    {
        private const string Separator = "..";

        public override string Name => "DynamicRange";

        public override DateTimeRange CalculateFromInput(string input = "")
        {
            string[] splitByTwoPoints = input.Split(separator: Separator);

            DateTimeRange startDateTimeRange =
                OtherCalculations
                .FirstOrDefault(predicate: m => m.DoesMatchInput(input: splitByTwoPoints.First()))
                .CalculateFromInput(input: splitByTwoPoints.First());

            DateTimeRange endDateTimeRange =
                OtherCalculations
                .FirstOrDefault(predicate: m => m.DoesMatchInput(input: splitByTwoPoints.Last()))
                .CalculateFromInput(input: splitByTwoPoints.Last());

            return new DateTimeRange(
                start: startDateTimeRange.Start,
                end: endDateTimeRange.End);
        }

        public override bool DoesMatchInput(string input)
        {
            string[] splitByTwoPoints = input.Split(separator: Separator);
            return
                    OtherCalculations.Any(predicate: m => m.DoesMatchInput(input: splitByTwoPoints.First())) &&
                    OtherCalculations.Any(predicate: m => m.DoesMatchInput(input: splitByTwoPoints.Last()));
        }

        public override bool NeedsOtherCalculations => true;
    }
}