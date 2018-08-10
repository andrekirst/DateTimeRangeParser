using System;

namespace DateTimeRange
{
    public class DefaultDateTimeProvider : IDateTimeProvider
    {
        public DateTime Today => DateTime.Today;
    }
}