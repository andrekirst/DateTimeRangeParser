using System;

namespace DateTimeRange.DateTimeRangeCalculators
{
    public class LastWeekCalculator : IDateTimeRangeCalculator
    {
        //public string RangeSelectorName => "LastWeek";

        //public DateTimeRange CalculateToRelatedDateTime(DateTime dateTime)
        //{
        //    DateTime start = dateTime
        //            .AddDays(value: -(int)dateTime.DayOfWeek + 1 - 7);
        //    DateTime end = dateTime
        //        .AddDays(value: 7 % (int)dateTime.DayOfWeek - 7);

        //    return new DateTimeRange
        //    {
        //        Start = start,
        //        End = end
        //    };
        //}

        public LastWeekCalculator(IDateTimeProvider dateTimeProvider)
        {
            DateTimeProvider = dateTimeProvider;
        }

        public IDateTimeProvider DateTimeProvider { get; set; }
        public string Name => "LastWeek";
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