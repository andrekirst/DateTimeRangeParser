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
    }
}
