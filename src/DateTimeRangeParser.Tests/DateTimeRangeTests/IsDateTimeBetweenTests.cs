using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace DateTimeRangeParser.Tests.DateTimeRangeTests
{
    public class IsDateTimeBetweenTests
    {
        [Fact]
        public void DateTime_is_between_Start_and_End_Expect_true()
        {
            DateTimeRange range = new DateTimeRange(
                start: new DateTime(2019, 1, 1),
                end: new DateTime(2019, 1, 6));

            DateTime testDate = new DateTime(2019, 1, 3);

            range.IsDateTimeBetween(testDate).ShouldBeTrue();
        }

        [Fact]
        public void DateTime_is_before_Start_Expect_false()
        {
            DateTimeRange range = new DateTimeRange(
                start: new DateTime(2019, 1, 1),
                end: new DateTime(2019, 1, 6));

            DateTime testDate = new DateTime(2018, 12, 1);

            range.IsDateTimeBetween(testDate).ShouldBeFalse();
        }
    }
}
