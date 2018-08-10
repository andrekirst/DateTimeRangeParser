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

        public IDateTimeProvider DateTimeProvider { get; set; }

        public string Name => "Yesterday";

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