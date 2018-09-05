using System;
using DateTimeRangeParser.Extensions;
using Xunit;

namespace DateTimeRangeParser.Tests.ExtensionTests.DateTimeExtensionTests
{
    public class GetFirstDayOfYearTests
    {
        [Fact]
        public void Year_2018_Expect_01_01_2018()
        {
            DateTime actual = new DateTime(year: 2018, month: 1, day: 1).GetFirstMondayOfYear();

            DateTime expected = new DateTime(year: 2018, month: 1, day: 1);

            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact]
        public void Year_2017_Expect_02_01_2017()
        {
            DateTime actual = new DateTime(year: 2017, month: 1, day: 1).GetFirstMondayOfYear();

            DateTime expected = new DateTime(year: 2017, month: 1, day: 2);

            Assert.Equal(expected: expected, actual: actual);
        }
    }
}
