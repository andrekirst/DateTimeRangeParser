using System;
using Moq;
using Xunit;

namespace DateTimeRange.Tests
{
    public class RangeExtractorTests
    {
        [Fact]
        public void Today_is_11_04_1986_input_is_Yesterday_Expect_10_04_1986_to_10_04_1986()
        {
            Mock<IDateTimeProvider> mock = new Mock<IDateTimeProvider>();

            RangeExtractor rangeExtractor = new RangeExtractor(dateTime: mock.Object);

            mock
                .SetupGet(expression: m => m.Today)
                .Returns(value: new DateTime(year: 1986, month: 4, day: 11));

            DateTimeRange actual = rangeExtractor.GenerateDateTimeRangeFromInput(input: "Yesterday");

            DateTimeRange expected = new DateTimeRange
            {
                Start = new DateTime(year: 1986, month: 4, day: 10),
                End = new DateTime(year: 1986, month: 4, day: 10)
            };

            Assert.Equal(expected: expected.Start, actual: actual.Start);
            Assert.Equal(expected: expected.End, actual: actual.End);
        }

        [Fact]
        public void Today_is_11_04_1986_input_is_LastWeek_Expect_31_03_1986_to_06_04_1986()
        {
            Mock<IDateTimeProvider> mock = new Mock<IDateTimeProvider>();

            RangeExtractor rangeExtractor = new RangeExtractor(dateTime: mock.Object);

            mock
                .SetupGet(expression: m => m.Today)
                .Returns(value: new DateTime(year: 1986, month: 4, day: 11));

            DateTimeRange actual = rangeExtractor.GenerateDateTimeRangeFromInput(input: "LastWeek");

            DateTimeRange expected = new DateTimeRange
            {
                Start = new DateTime(year: 1986, month: 3, day: 31),
                End = new DateTime(year: 1986, month: 4, day: 6)
            };

            Assert.Equal(expected: expected.Start, actual: actual.Start);
            Assert.Equal(expected: expected.End, actual: actual.End);
        }
    }
}
