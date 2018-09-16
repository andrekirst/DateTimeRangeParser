using System;
using System.Linq;
using System.Collections.Generic;
using DateTimeRangeParser.Calculations;
using Moq;
using Shouldly;
using Xunit;

namespace DateTimeRangeParser.Tests.CalculationTests
{
    public class DynamicRangeCalculatorTests
    {
        [Fact]
        public void Yesterday_to_Today_Test_DoesMatchInput_Expect_True()
        {
            Mock<IDateTimeProvider> mockDateTimeProvider = new Mock<IDateTimeProvider>();

            DynamicRangeCalculator systemUnderTest = new DynamicRangeCalculator()
            {
                OtherCalculations = LoadCalculations(mockDateTimeProvider: mockDateTimeProvider)
            };

            systemUnderTest
                .DoesMatchInput(input: "yesterday->today")
                .ShouldBeTrue();
        }

        [Fact]
        public void Yesterday_to_Today_Test_DoesMatchInput_with_wrong_splitter_Expect_false()
        {
            Mock<IDateTimeProvider> mockDateTimeProvider = new Mock<IDateTimeProvider>();

            DynamicRangeCalculator systemUnderTest = new DynamicRangeCalculator()
            {
                OtherCalculations = LoadCalculations(mockDateTimeProvider: mockDateTimeProvider)
            };

            systemUnderTest
                .DoesMatchInput(input: "yesterday..today")
                .ShouldBeFalse();
        }

        [Fact]
        public void Bla_to_Blabla_Test_DoesMatchInput_with_non_existing_Calculations_Expect_false()
        {
            Mock<IDateTimeProvider> mockDateTimeProvider = new Mock<IDateTimeProvider>();

            mockDateTimeProvider
                .SetupGet(expression: m => m.Today)
                .Returns(value: new DateTime(year: 1986, month: 4, day: 11));

            DynamicRangeCalculator systemUnderTest = new DynamicRangeCalculator()
            {
                OtherCalculations = LoadCalculations(mockDateTimeProvider: mockDateTimeProvider)
            };

            systemUnderTest
                .DoesMatchInput(input: "bla->blabla")
                .ShouldBeFalse();
        }

        [Fact]
        public void Yesterday_to_Today_Today_is_11_04_1986_Expect_10_04_1986_to_11_04_1986()
        {
            Mock<IDateTimeProvider> mockDateTimeProvider = new Mock<IDateTimeProvider>();

            mockDateTimeProvider
                .SetupGet(expression: m => m.Today)
                .Returns(value: new DateTime(year: 1986, month: 4, day: 11));

            // TODO Umbauen...ich will ja nicht das Laden testen und auch nicht andere Implementierungen (Seperation!!!)
            List<DateTimeRangeCalculatorBase> calculations = LoadCalculations(mockDateTimeProvider: mockDateTimeProvider);

            DynamicRangeCalculator systemUnderTest = new DynamicRangeCalculator()
            {
                OtherCalculations = calculations,
                DateTimeProvider = mockDateTimeProvider.Object
            };

            DateTimeRange actual = systemUnderTest.CalculateFromInput(input: "yesterday->today");

            DateTimeRange expected = new DateTimeRange(
                start: new DateTime(year: 1986, month: 4, day: 10),
                end: new DateTime(year: 1986, month: 4, day: 11));

            actual.ShouldBe(expected: expected);
        }

        private List<DateTimeRangeCalculatorBase> LoadCalculations(Mock<IDateTimeProvider> mockDateTimeProvider)
        {
            CalculationsLoader calculationsLoader = new CalculationsLoader();
            List<DateTimeRangeCalculatorBase> calculations = calculationsLoader
                .LoadCalculations()
                .Where(c => !(c is DynamicRangeCalculator))
                .ToList();
            foreach (var item in calculations)
            {
                item.DateTimeProvider = mockDateTimeProvider.Object;
            }

            return calculations;
        }
    }
}
