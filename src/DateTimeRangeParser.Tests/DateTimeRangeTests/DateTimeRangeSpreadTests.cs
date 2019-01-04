using System;
using System.Linq;
using Xunit;
using Shouldly;

namespace DateTimeRangeParser.Tests.DateTimeRangeTests
{
    public class DateTimeRangeSpreadTests
    {
        [Fact]
        public void Spread_1_Day()
        {
            DateTimeRange range = new DateTimeRange(
                new DateTime(2019, 1, 5),
                new DateTime(2019, 1, 6));

            DateTimeRange actual = range.SpreadByDays(days: 1);
            actual.Start.ShouldBe(new DateTime(2019, 1, 4));
            actual.End.ShouldBe(new DateTime(2019, 1, 7));
        }

        [Fact]
        public void Spread_1_Day_for_Start_and_2_Days_for_End()
        {
            DateTimeRange range = new DateTimeRange(
                new DateTime(2019, 1, 5),
                new DateTime(2019, 1, 6));

            DateTimeRange actual = range.SpreadByDays(daysStart: 1, daysEnd: 2);
            actual.Start.ShouldBe(new DateTime(2019, 1, 4));
            actual.End.ShouldBe(new DateTime(2019, 1, 8));
        }
    }
}
