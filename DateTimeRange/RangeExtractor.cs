using System;

namespace DateTimeRange
{
    public class RangeExtractor
    {
        private readonly IDateTimeProvider _dateTime;

        public RangeExtractor(IDateTimeProvider dateTime)
        {
            _dateTime = dateTime;
        }

        public DateTimeRange GenerateDateTimeRangeFromInput(string input)
        {
            switch (input)
            {
                case "Yesterday":
                    {
                        return GetRangeForYesterday();
                    }
                case "LastWeek":
                    {
                        return GetRangeForLastWeek();
                    }
            }

            throw new NotImplementedException();
        }

        private DateTimeRange GetRangeForLastWeek()
        {
            DateTime start = _dateTime
                .Today
                .AddDays(value: -(int)_dateTime.Today.DayOfWeek + 1 - 7);
            DateTime end = _dateTime
                .Today
                .AddDays(value: 7 % (int)_dateTime.Today.DayOfWeek - 7);

            return new DateTimeRange
            {
                Start = start,
                End = end
            };
        }

        private DateTimeRange GetRangeForYesterday()
        {
            DateTime dateTime = _dateTime.Today.AddDays(value: -1);

            return new DateTimeRange
            {
                Start = dateTime,
                End = dateTime
            };
        }
    }
}