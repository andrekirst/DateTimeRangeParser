using System;
using System.Linq;
using Xunit;
using DateTimeRange.Extensions;

namespace DateTimeRange.Tests.ExtensionTests.DateTimeExtensionTests
{
    public class GetFirstDayOfYearTests
    {
        [Fact]
        public void Year_2018_Expect_01_01_2018()
        {
            DateTime actual = new DateTime(2018, 1, 1).GetFirstMondayOfYear();

            DateTime expected = new DateTime(2018, 1, 1);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Year_2017_Expect_02_01_2017()
        {
            DateTime actual = new DateTime(2017, 1, 1).GetFirstMondayOfYear();

            DateTime expected = new DateTime(2017, 1, 2);

            Assert.Equal(expected, actual);
        }
    }
}
