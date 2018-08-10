using System;

namespace DateTimeRange.DateTimeRangeCalculators
{
    public class ThisMonthCalculator : IDateTimeRangeCalculator
    {
        //public string RangeSelectorName => "ThisMonth";

        //public DateTimeRange CalculateToRelatedDateTime(DateTime dateTime)
        //{
        //    DateTime nextMonthSameDay = dateTime.Date.AddMonths(months: 1);
        //    DateTime end = dateTime.AddMonths(months: 1).AddDays(value: -nextMonthSameDay.Day);

        //    return new DateTimeRange
        //    {
        //        Start = dateTime.AddDays(value: -dateTime.Day + 1),
        //        End = end
        //    };
        //}

        public IDateTimeProvider DateTimeProvider { get; set; }
        public string Name => "ThisMonth";
        public DateTimeRange CalculateFromInput(string input)
        {
            throw new NotImplementedException();
        }

        public bool DoesMatchInput(string input)
        {
            throw new NotImplementedException();
        }
    }
}