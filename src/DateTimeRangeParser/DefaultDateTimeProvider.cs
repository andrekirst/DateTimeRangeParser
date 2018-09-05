using System;

namespace DateTimeRangeParser
{
    public class DefaultDateTimeProvider : IDateTimeProvider
    {
        public DateTime Today => DateTime.Today;
    }
}