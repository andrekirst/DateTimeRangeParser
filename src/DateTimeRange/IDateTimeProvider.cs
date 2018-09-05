using System;

namespace DateTimeRangeParser
{
    public interface IDateTimeProvider
    {
        DateTime Today { get; }
    }
}