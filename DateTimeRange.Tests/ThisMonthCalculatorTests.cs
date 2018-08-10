using System;
using System.Linq;
using DateTimeRange.DateTimeRangeCalculators;
using Moq;
using Xunit;

namespace DateTimeRange.Tests
{
    public class ThisMonthCalculatorTests
    {
        private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;

        public ThisMonthCalculatorTests()
        {
            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
        }

        [Fact]
        public void ThisMonthCalculatorTests_Today_is_10_08_2018_Expect_01_08_2018_31_08_2018()
        {
            ThisMonthCalculator calculator = new ThisMonthCalculator(dateTimeProvider: _mockDateTimeProvider.Object);
            DateTimeRange actual = calculator.CalculateFromInput(input: "ThisMonth");

            DateTimeRange expected = new DateTimeRange
            {
                Start = new DateTime(year: 2018, month: 8, day: 1),
                End = new DateTime(year: 2018, month: 8, day: 31)
            };

            Assert.Equal(expected: expected, actual: actual);
        }
    }
}
