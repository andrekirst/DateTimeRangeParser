using System;
using System.Linq;

namespace DateTimeRange.DateTimeRangeCalculators
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
                .FirstOrDefault(m => m.DoesMatchInput(splitByTwoPoints.First()))
                .CalculateFromInput(input: splitByTwoPoints.First());

            DateTimeRange endDateTimeRange =
                OtherCalculations
                .FirstOrDefault(m => m.DoesMatchInput(splitByTwoPoints.Last()))
                .CalculateFromInput(input: splitByTwoPoints.Last());

            return new DateTimeRange(
                start: startDateTimeRange.Start,
                end: endDateTimeRange.End);
        }

        public override bool DoesMatchInput(string input)
        {
            string[] splitByTwoPoints = input.Split(separator: Separator);
            return
                    OtherCalculations.Any(m => m.DoesMatchInput(splitByTwoPoints.First())) &&
                    OtherCalculations.Any(m => m.DoesMatchInput(splitByTwoPoints.Last()));
        }

        public override bool NeedsOtherCalculations => true;
    }
}