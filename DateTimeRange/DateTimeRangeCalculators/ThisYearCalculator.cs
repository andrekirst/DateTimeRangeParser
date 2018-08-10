using System;

namespace DateTimeRange.DateTimeRangeCalculators
{
    public class ThisYearCalculator : IDateTimeRangeCalculator
    {
        //public string RangeSelectorName => "ThisYear";

        //public DateTimeRange CalculateToRelatedDateTime(DateTime dateTime)
        //{
        //    return new DateTimeRange
        //    {
        //        Start = new DateTime(year: dateTime.Year, month: 1, day: 1),
        //        End = new DateTime(year: dateTime.Year, month: 12, day: 31)
        //    };
        //}

        public IDateTimeProvider DateTimeProvider { get; set; }

        public string Name => "ThisYear";

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