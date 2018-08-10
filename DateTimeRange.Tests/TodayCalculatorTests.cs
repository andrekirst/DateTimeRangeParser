using System;
using System.Text.RegularExpressions;
using DateTimeRange.DateTimeRangeCalculators;
using Moq;
using Xunit;

namespace DateTimeRange.Tests
{
    public class TodayCalculatorTests
    {
        private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;

        public TodayCalculatorTests()
        {
            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
        }

        [Theory]
        [InlineData("today")]
        [InlineData("TODAY")]
        [InlineData("Today")]
        public void TodayCalculatorTests_Validate_Regex_Pattern_for_Today(string input)
        {
            TodayCalculator todayCalculator = new TodayCalculator(dateTimeProvider: _mockDateTimeProvider.Object);

            string regexPattern = todayCalculator.DetectionRegexPattern;

            Assert.True(condition: Regex.IsMatch(
                input: input,
                pattern: regexPattern),
                userMessage: $"Input was: {input} for pattern: \"{regexPattern}\"");
        }

        [Fact]
        public void TodayCalculatorTests_Today_is_11_04_1986_Expect_11_04_1986_to_11_04_1986()
        {
            _mockDateTimeProvider
                .SetupGet(expression: m => m.Today)
                .Returns(value: new DateTime(year: 1986, month: 4, day: 11));

            TodayCalculator todayCalculator = new TodayCalculator(dateTimeProvider: _mockDateTimeProvider.Object);

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
