using Moq;

namespace DateTimeRangeParser.Tests
{
    public class ThisMonthCalculatorTests
    {
        private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;

        public ThisMonthCalculatorTests()
        {
            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
        }
    }
}
