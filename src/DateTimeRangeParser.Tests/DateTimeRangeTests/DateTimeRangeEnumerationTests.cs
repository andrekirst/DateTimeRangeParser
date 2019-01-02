using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Shouldly;

namespace DateTimeRangeParser.Tests.DateTimeRangeTests
{
    public class DateTimeRangeEnumerationTests
    {
        [Fact(DisplayName = "Enumerate three days")]
        public void Enumerate_three_Days()
        {
            DateTimeRange dateTimeRange = new DateTimeRange(
                start: new DateTime(2019, 1, 1),
                end: new DateTime(2019, 1, 3));

            List<DateTime> actual = ((IEnumerable<DateTime>)dateTimeRange).ToList();

            actual.ShouldBe(new List<DateTime>
            {
                new DateTime(2019, 1, 1),
                new DateTime(2019, 1, 2),
                new DateTime(2019, 1, 3)
            });
        }
    }
}
