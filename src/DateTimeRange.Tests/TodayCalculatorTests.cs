using System;
using DateTimeRangeParser.Calculations;
using Moq;
using Xunit;

namespace DateTimeRangeParser.Tests
{
    public class TodayCalculatorTests
    {
        private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;

        public TodayCalculatorTests()
        {
            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
        }

        [Fact]
        public void TodayCalculatorTests_Today_is_11_04_1986_Expect_11_04_1986_to_11_04_1986()
        {
            _mockDateTimeProvider
                .SetupGet(expression: m => m.Today)
                .Returns(value: new DateTime(year: 1986, month: 4, day: 11));

            TodayCalculator todayCalculator = new TodayCalculator
            {
                DateTimeProvider = _mockDateTimeProvider.Object
            };

            DateTimeRange actual = todayCalculator.CalculateFromInput();

            DateTimeRange expected = new DateTimeRange
            {
                Start = new DateTime(year: 1986, month: 4, day: 11),
                End = new DateTime(year: 1986, month: 4, day: 11)
            };

            Assert.Equal(expected: expected, actual: actual);
        }
    }
}
