using System;
using DateTimeRange.DateTimeRangeCalculators;
using Moq;
using Xunit;

namespace DateTimeRange.Tests
{
    public class ThisYearCalculatorTests
    {
        private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;

        public ThisYearCalculatorTests()
        {
            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
        }

        [Fact]
        public void ThisYearCalculatorTests_11_04_1986_Expect_01_01_1986_to_31_12_1986()
        {
            ThisYearCalculator calculator = new ThisYearCalculator(dateTimeProvider: _mockDateTimeProvider.Object);
            //DateTimeRange actual = calculator.CalculateToRelatedDateTime(dateTime: new DateTime(year: 1986, month: 4, day: 11));

            //DateTimeRange expected = new DateTimeRange
            //{
            //    Start = new DateTime(year: 1986, month: 1, day: 1),
            //    End = new DateTime(year: 1986, month: 12, day: 31)
            //};

            //Assert.Equal(expected: expected, actual: actual);
            throw new NotImplementedException();
        }
    }
}
