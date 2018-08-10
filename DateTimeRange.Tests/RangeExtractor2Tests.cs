using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Moq;
using Xunit;

namespace DateTimeRange.Tests
{
    public class RangeExtractor2Tests
    {
        private readonly Mock<IDateTimeProvider> _mockDateTimeSource;

        private class MockImplementationTodayCalculator : IDateTimeRangeCalculator
        {
            public MockImplementationTodayCalculator(IDateTimeProvider dateTimeProvider)
            {
                DateTimeProvider = dateTimeProvider;
            }

            public IDateTimeProvider DateTimeProvider { get; }
            public string Name => "Today";
            public string DetectionRegexPattern { get; }
            public DateTimeRange CalculateFromInput(string input)
            {
                return new DateTimeRange
                {
                    Start = new DateTime(year: 1986, month: 4, day: 11),
                    End = new DateTime(year: 1986, month: 4, day: 11)
                };
            }
        }

        public RangeExtractor2Tests()
        {
            _mockDateTimeSource = new Mock<IDateTimeProvider>();
        }

        [Fact]
        public void Test_Execution_Expect_SelectorNames()
        {
            List<IDateTimeRangeCalculator> calculators = new List<IDateTimeRangeCalculator>
            {
                new MockImplementationTodayCalculator(dateTimeProvider: _mockDateTimeSource.Object)
            };
            _mockDateTimeSource
                .SetupGet(m => m.Today)
                .Returns(new DateTime(1986, 4, 11));

            RangeExtractor2 rangeExtractor2 = new RangeExtractor2(
                dateTimeSource: _mockDateTimeSource.Object,
                calculators: calculators);

            ReadOnlyCollection<string> expectedSelectorNames = new List<string> { "Today" }.AsReadOnly();
            Assert.Equal(
                expected: expectedSelectorNames,
                actual: rangeExtractor2.ImplementedCalculatorNames);
        }

        [Fact]
        public void Test_Execution_Expect_Calculation_For_Input()
        {
            List<IDateTimeRangeCalculator> calculators = new List<IDateTimeRangeCalculator>
            {
                new MockImplementationTodayCalculator(dateTimeProvider: _mockDateTimeSource.Object)
            };

            _mockDateTimeSource
                .SetupGet(m => m.Today)
                .Returns(new DateTime(1986, 4, 11));

            RangeExtractor2 rangeExtractor2 = new RangeExtractor2(
                dateTimeSource: _mockDateTimeSource.Object,
                calculators: calculators);

            DateTimeRange actual = rangeExtractor2.GenerateDateTimeRangeFromInput(input: "Today");

            DateTimeRange expected = new DateTimeRange
            {
                Start = new DateTime(year: 1986, month: 4, day: 11),
                End = new DateTime(year: 1986, month: 4, day: 11)
            };

            Assert.Equal(
                expected: expected,
                actual: actual);
        }
    }
}
