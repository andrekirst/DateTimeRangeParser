using DateTimeRange.DateTimeRangeCalculators;
using Xunit;
using Shouldly;
using Moq;
using System;
using System.Collections.Generic;

namespace DateTimeRange.Tests
{
    public class DynamicRangeCalculatorTests
    {
        [Fact]
        public void Yesterday_to_Today_Test_DoesMatchInput_Expect_True()
        {
            CalculationsLoader calculationsLoader = new CalculationsLoader();

            DynamicRangeCalculator systemUnderTest = new DynamicRangeCalculator()
            {
                OtherCalculations = calculationsLoader.LoadCalculations()
            };

            systemUnderTest
                .DoesMatchInput("yesterday..today")
                .ShouldBeTrue();
        }

        [Fact]
        public void Yesterday_to_Today_Test_DoesMatchInput_with_wrong_splitter_Expect_false()
        {
            CalculationsLoader calculationsLoader = new CalculationsLoader();

            DynamicRangeCalculator systemUnderTest = new DynamicRangeCalculator()
            {
                OtherCalculations = calculationsLoader.LoadCalculations()
            };

            systemUnderTest
                .DoesMatchInput("yesterday->today")
                .ShouldBeFalse();
        }

        [Fact]
        public void Bla_to_Blabla_Test_DoesMatchInput_with_non_existing_Calculations_Expect_false()
        {
            CalculationsLoader calculationsLoader = new CalculationsLoader();

            DynamicRangeCalculator systemUnderTest = new DynamicRangeCalculator()
            {
                OtherCalculations = calculationsLoader.LoadCalculations()
            };

            systemUnderTest
                .DoesMatchInput("bla->blabla")
                .ShouldBeFalse();
        }

        [Fact]
        public void Yesterday_to_Today_Today_is_11_04_1986_Expect_10_04_1986_to_11_04_1986()
        {
            Mock<IDateTimeProvider> _mockDateTimeProvider = new Mock<IDateTimeProvider>();

            _mockDateTimeProvider
                .SetupGet(m => m.Today)
                .Returns(new DateTime(1986, 4, 11));

            // TODO Umbauen...ich will ja nicht das Laden testen und auch nicht andere Implementierungen (Seperation!!!)
            List<DateTimeRangeCalculatorBase> calculations = LoadCalculations(_mockDateTimeProvider);

            DynamicRangeCalculator systemUnderTest = new DynamicRangeCalculator()
            {
                OtherCalculations = calculations,
                DateTimeProvider = _mockDateTimeProvider.Object
            };

            DateTimeRange actual = systemUnderTest.CalculateFromInput(input: "yesterday..today");

            DateTimeRange expected = new DateTimeRange(
                start: new DateTime(1986, 4, 10),
                end: new DateTime(1986, 4, 11));

            actual.ShouldBe(expected: expected);
        }

        private static List<DateTimeRangeCalculatorBase> LoadCalculations(Mock<IDateTimeProvider> mockDateTimeProvider)
        {
            CalculationsLoader calculationsLoader = new CalculationsLoader();
            List<DateTimeRangeCalculatorBase> calculations = calculationsLoader.LoadCalculations();
            foreach (var item in calculations)
            {
                item.DateTimeProvider = mockDateTimeProvider.Object;
            }

            return calculations;
        }
    }
}
