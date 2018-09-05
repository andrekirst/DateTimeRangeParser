using System;

namespace DateTimeRange.DateTimeRangeCalculators
{
    public class ThisMonthCalculator : DateTimeRangeCalculatorBase
    {
        public override string Name => "ThisMonth";

        public override DateTimeRange CalculateFromInput(string input = "")
        {
            DateTime nextMonthSameDay = Today.Date.AddMonths(months: 1);
            DateTime end = Today.AddMonths(months: 1).AddDays(value: -nextMonthSameDay.Day);

            return new DateTimeRange
            {
                Start = Today.AddDays(value: -Today.Day + 1),
                End = end
            };
        }

        public override bool DoesMatchInput(string input)
        {
            return EqualsLowerMatch(
                input: input,
                match: "thismonth");
        }
    }
}