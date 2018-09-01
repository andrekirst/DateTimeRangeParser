using System;

namespace DateTimeRange
{
    public interface IDateTimeProvider
    {
        DateTime Today { get; }
    }
}