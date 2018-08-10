using System;

namespace DateTimeRange.DateTimeRangeCalculators
{
    public class CurrentWeekCalculator : IDateTimeRangeCalculator
    {
        //public string RangeSelectorName => "CurrentWeek";

        //public DateTimeRange CalculateToRelatedDateTime(DateTime dateTime)
        //{
        //    DateTime start = dateTime
        //            .AddDays(value: -(int)dateTime.DayOfWeek + 1);
        //    DateTime end = dateTime
        //        .AddDays(value: 7 % (int)dateTime.DayOfWeek);

        //    return new DateTimeRange
        //    {
        //        Start = start,
        //        End = end
        //    };
        //}

        public IDateTimeProvider DateTimeProvider { get; set; }

        public string Name => "CurrentWeek";

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