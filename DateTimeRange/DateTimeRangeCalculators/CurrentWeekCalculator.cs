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

        public CurrentWeekCalculator(IDateTimeProvider dateTimeProvider)
        {
            DateTimeProvider = dateTimeProvider;
        }

        public IDateTimeProvider DateTimeProvider { get; }
        public string Name => "CurrentWeek";
        public string DetectionRegexPattern { get; }
        public DateTimeRange CalculateFromInput(string input)
        {
            throw new NotImplementedException();
        }
    }
}