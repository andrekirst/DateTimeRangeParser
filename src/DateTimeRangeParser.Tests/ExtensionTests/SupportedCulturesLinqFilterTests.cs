using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Moq;
using Xunit;
using DateTimeRangeParser.Extensions;
using Shouldly;

namespace DateTimeRangeParser.Tests.ExtensionTests
{
    public class SupportedCulturesLinqFilterTests
    {
        private readonly Mock<DateTimeRangeCalculatorBase> _cultureEnCalculation;
        private readonly Mock<DateTimeRangeCalculatorBase> _cultureDeCalculation;
        private readonly Mock<DateTimeRangeCalculatorBase> _cultureNullCalculation;

        public SupportedCulturesLinqFilterTests()
        {
            _cultureEnCalculation = new Mock<DateTimeRangeCalculatorBase>();
            _cultureEnCalculation
                .SetupGet(expression: m => m.SupportedCultures)
                .Returns(value: new List<CultureInfo> { CultureInfo.GetCultureInfoByIetfLanguageTag(name: "en") });

            _cultureDeCalculation = new Mock<DateTimeRangeCalculatorBase>();
            _cultureDeCalculation
                .SetupGet(expression: m => m.SupportedCultures)
                .Returns(value: new List<CultureInfo> { CultureInfo.GetCultureInfoByIetfLanguageTag(name: "de") });

            _cultureNullCalculation = new Mock<DateTimeRangeCalculatorBase>();
            _cultureNullCalculation
                .SetupGet(expression: m => m.SupportedCultures)
                .Returns(value: (List<CultureInfo>)null);
        }

        [Fact]
        public void To_Loading_Cultures_is_null_load_all_3_existing_Implementations()
        {
            List<DateTimeRangeCalculatorBase> calculations = new List<DateTimeRangeCalculatorBase>
            {
                _cultureEnCalculation.Object,
                _cultureDeCalculation.Object,
                _cultureNullCalculation.Object
            };

            var actual = calculations.FilterBySupportedCultures(toLoadingCultures: null);

            actual.Count().ShouldBe(expected: 3);
        }

        [Fact]
        public void To_Loading_Cultures_is_de_load_2_of_3_existing_Implementations()
        {
            List<DateTimeRangeCalculatorBase> calculations = new List<DateTimeRangeCalculatorBase>
            {
                _cultureEnCalculation.Object,
                _cultureDeCalculation.Object,
                _cultureNullCalculation.Object
            };

            List<DateTimeRangeCalculatorBase> actual =
                calculations
                    .FilterBySupportedCultures(toLoadingCultures: new List<CultureInfo>
                    {
                        CultureInfo.GetCultureInfoByIetfLanguageTag(name: "de")
                    })
                    .ToList();

            actual.Count().ShouldBe(expected: 2);

            actual
                .Any(predicate: b => b.SupportedCultures == null)
                .ShouldBeTrue();

            actual.Any(predicate: b => b.SupportedCultures.Contains(item: CultureInfo.GetCultureInfoByIetfLanguageTag(name: "de")))
                .ShouldBeTrue();
        }

        [Fact]
        public void To_Loading_Cultures_is_1031_load_2_of_3_existing_Implementations()
        {
            List<DateTimeRangeCalculatorBase> calculations = new List<DateTimeRangeCalculatorBase>
            {
                _cultureEnCalculation.Object,
                _cultureDeCalculation.Object,
                _cultureNullCalculation.Object
            };

            List<DateTimeRangeCalculatorBase> actual =
                calculations
                    .FilterBySupportedCultures(toLoadingCultures: new List<CultureInfo>
                    {
                        CultureInfo.GetCultureInfo(culture: 1031)
                    })
                    .ToList();

            actual.Count().ShouldBe(expected: 2);

            actual
                .Any(predicate: b => b.SupportedCultures == null)
                .ShouldBeTrue();

            actual.Any(predicate: b => b.SupportedCultures.Contains(item: CultureInfo.GetCultureInfoByIetfLanguageTag(name: "de")))
                .ShouldBeTrue();
        }
    }
}
