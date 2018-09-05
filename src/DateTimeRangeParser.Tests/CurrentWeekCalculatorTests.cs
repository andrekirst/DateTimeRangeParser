using System;
using DateTimeRangeParser.Calculations;
using Moq;
using Xunit;

namespace DateTimeRangeParser.Tests
{
    public class CurrentWeekCalculatorTests
    {
        private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;

        public CurrentWeekCalculatorTests()
        {
            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
        }

        [Fact]
        public void CurrentWeekCalculatorTests_Today_is_03_08_2018_Expect_13_08_2018_to_19_08_2018()
        {
            _mockDateTimeProvider
                .SetupGet(expression: m => m.Today)
                .Returns(value: new DateTime(year: 2018, month: 8, day: 13));

            CurrentWeekCalculator systemUnderTest = new CurrentWeekCalculator
            {
                DateTimeProvider = _mockDateTimeProvider.Object
            };

            DateTimeRange actual = systemUnderTest.CalculateFromInput(input: "currentweek");

            DateTimeRange expected = new DateTimeRange
            {
                Start = new DateTime(year: 2018, month: 8, day: 13),
                End = new DateTime(year: 2018, month: 8, day: 19)
            };

            Assert.Equal(expected: expected, actual: actual);
        }
    }
}
