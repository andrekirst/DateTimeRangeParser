using System;

namespace DateTimeRange.DateTimeRangeCalculators
{
    public class YesterdayCalculator : IDateTimeRangeCalculator
    {
        //public string RangeSelectorName => "Yesterday";
        //public DateTimeRange CalculateToRelatedDateTime(DateTime dateTime)
        //{
        //    DateTime yesterday = dateTime
        //        .AddDays(value: -1);

        //    return new DateTimeRange
        //    {
        //        Start = yesterday,
        //        End = yesterday
        //    };
        //}


        public YesterdayCalculator(IDateTimeProvider dateTimeProvider)
        {
            DateTimeProvider = dateTimeProvider;
        }

        public IDateTimeProvider DateTimeProvider { get; }
        public string Name => "Yesterday";
        public string DetectionRegexPattern { get; }
        public DateTimeRange CalculateFromInput(string input)
        {
            throw new NotImplementedException();
        }
    }
}