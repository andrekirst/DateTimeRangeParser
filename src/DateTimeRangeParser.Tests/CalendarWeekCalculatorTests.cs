using System;
using DateTimeRangeParser.Calculations;
using Moq;
using Xunit;

namespace DateTimeRangeParser.Tests
{
    public class CalendarWeekCalculatorTests
    {
        private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;

        public CalendarWeekCalculatorTests()
        {
            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
            _mockDateTimeProvider
                .Setup(expression: m => m.Today)
                .Returns(value: DateTime.Today);
        }

        [Fact]
        public void CalendarWeekCalculatorTests_CW3_2018_Expect_15_01_2018_to_21_01_2018()
        {
            CalendarWeekCalculator systemUnderTest = new CalendarWeekCalculator
            {
                DateTimeProvider = _mockDateTimeProvider.Object
            };

            DateTimeRange actual = systemUnderTest.CalculateFromInput(input: "CW3.2018");

            DateTimeRange expected = new DateTimeRange
            {
                Start = new DateTime(year: 2018, month: 1, day: 15),
                End = new DateTime(year: 2018, month: 1, day: 21)
            };

            Assert.Equal(
                expected: expected,
                actual: actual);
        }
    }
}
